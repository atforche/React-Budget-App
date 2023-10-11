import json
import os
import shutil
import subprocess
from .Exceptions import PlatformSpecificException

"""
Base class containing common helper functions used to run the needed scripts.

Certain functions require platform specific code, which must be implemented in a derived
platform specific class. Attempting to call one of these methods from this base class
will raise a PlatformSpecificException.
"""
class CommonFunctions():

    """
    Constructs a new instance of this class

    Args:
        base_repository_path: name of the base repository path where the application code lives
    """
    def __init__(self, base_repository_path: str):
        # Base path where the application code lives
        self.base_repository_path: str = base_repository_path

        # Path to where the configuration file lives
        self.config_file_path: str = os.path.abspath(self.base_repository_path + "\\Configuration\\config.json")

        # Path to where the schema project lives
        self.schema_directory: str = os.path.abspath(self.base_repository_path + "\\Schema")


    """
    Deploys either the local or published version of the software.

    Args:
        published_version: If True, deploy the local version and then publish it. Otherwise, just deploy the local version.
    """
    def deploy_version(self, published_version: bool):
        # Verify that we have the correct permissions
        self.verify_running_as_admin()

        # If the services exists, make sure they are stopped
        self.stop_services(published_version)

        # Run a clean to remove any build artifacts
        subprocess.run(["dotnet", "clean", "--nologo", "-v", "q"], check=True)

        # Build / Publish the solution
        if published_version:
            print("Publishing Solution...")
            subprocess.run(["dotnet", "publish", "-c", "Release", "--nologo", "-v", "q"], check=True)
        else:
            print("Building Solution...")
            subprocess.run(["dotnet", "build", "--nologo", "-v", "q"], check=True)

        # If the services don't exist, create them
        self.create_services(published_version)

        # Start the services
        self.start_services(published_version)


    """
    Verifies that the process is currently running with administrator/super-user privileges
    """
    def verify_running_as_admin(self):
        raise PlatformSpecificException()
    

    """
    Checks whether the service currently exists.

    Args:
        published_version: True to check for the published service, false to check for the local service.

    Returns:
        True if the service exists, false otherwise
    """
    def does_service_exist(self, published_version: bool) -> bool:
        raise PlatformSpecificException()
        

    """
    Creates the application services if they don't exist.

    Args:
        published_version: If True, create both the published and local services. Otherwise, just create the local service.
    """
    def create_services(self, published_version: bool):
        # Create the published service if needed and it doesn't exist
        if published_version and not self.does_service_exist(True):
            print(f"Creating Service: {self.get_config_setting('RestApiServiceName', True)}...")
            self.create_service(True)

        # Create the local service if it doesn't exist
        if not self.does_service_exist(False):
            print(f"Creating Service: {self.get_config_setting('RestApiServiceName', False)}...")
            self.create_service(False)
    

    """
    Creates an individual application service.

    Args:
        published_version: If True, create the published service. Otherwise, create the local service.
    """
    def create_service(self, service_name: str):
        raise PlatformSpecificException()
    

    """
    Stops the application services if they are currently running.

    Args:
        published_version: If True, stop both the published and local services. Otherwise, just stop the local service.
    """
    def stop_services(self, published_version: bool):
        # Stop the published service if needed, so long as it exists and is running
        if published_version and self.does_service_exist(True):
            print(f"Stopping Service: {self.get_config_setting('RestApiServiceName', True)}...")
            self.stop_service(True)

        # Stop the local service so long as it exists and is running
        if self.does_service_exist(False):
            print(f"Stopping Service: {self.get_config_setting('RestApiServiceName', False)}...")
            self.stop_service(False)
    

    """
    Stops an individual application service.

    Args:
        published_version: If True, stop the published service. Otherwise, stop the local service.
    """
    def stop_service(self, published_version: str):
        raise PlatformSpecificException()
    

    """
    Starts the application services.

    Args:
        published_version: If True, start both the published and local services. Otherwise, just start the local service.
    """
    def start_services(self, published_version: bool):
        # Start the published service if needed
        if published_version:
            print(f"Starting Service: {self.get_config_setting('RestApiServiceName', True)}...")
            self.start_service(True)

        # Start the local service
        print(f"Starting Service: {self.get_config_setting('RestApiServiceName', False)}...")
        self.start_service(False)
    

    """
    Starts an individual application service.

    Args:
        published_version: If True, start the published service. Otherwise, start the local service.
    """
    def start_service(self, service_name: str):
        raise PlatformSpecificException()


    """
    Creates a new Entity Framework Core migration in the Schema directory with the given name.

    Args:
        name: Name of the new migration to create
    """
    def create_new_migration(self, name: str):
        # Move to the Schema directory
        os.chdir(self.schema_directory)

        # Publish a new local version and create a new migration based on the local version
        local_schema_obj_directory = os.path.relpath(self.get_config_setting("AppDirectory", False) + "/obj/Schema")
        subprocess.run(["dotnet",
                        "ef",
                        "migrations",
                        "add",
                        name,
                        "--msbuildprojectextensionspath",
                        local_schema_obj_directory], check=True)
    

    """
    Removes an Entity Framework Core migration from the Schema directory. By default, it will remove
        the most recent migration. If the RemoveAll flag is set, all migrations will be removed along with
        the Migrations directory.

    Args:
        remove_all: If set, this function will remove all migrations and delete the Migrations directory
    """
    def remove_migration(self, remove_all: bool):
        # Move to the Schema directory
        os.chdir(self.schema_directory)

        # If remove_all is set, then just delete the Migrations directory. Otherwise, use the Dotnet utility
        if remove_all and os.path.exists("./Migrations"):
            shutil.rmtree("./Migrations")
        elif not remove_all:
            local_schema_obj_directory = os.path.relpath(self.get_config_setting("AppDirectory", False) + "/obj/Schema")
            subprocess.run(["dotnet", 
                            "ef", 
                            "migrations", 
                            "remove", 
                            "--msbuildprojectextensionspath", 
                            local_schema_obj_directory], check=True)


    """
    Grabs the specified configuration setting from the config file

    Args:
        name: Name of the configuration setting to retrieve
        published_version: If True, grab the setting for the published version. Otherwise, get the local version.

    Returns:
        The specified configuration setting from the config file
    """
    def get_config_setting(self, name: str, published_version: bool):
        # Ensure that the config.json file exists in the Configuration directory and grab its contents
        if not os.path.exists(self.config_file_path):
            raise Exception('Unable to find file named "config.json" in the Configuration directory.')
        with open(self.config_file_path) as config_file:
            configuration = json.load(config_file)

        # Ensure that the requested property exists in the config file and grab its contents
        if not name in configuration:
            raise Exception(f'The config file does not include the requested property "{name}"')
        config_setting = configuration[name]

        # Ensure that the requested property has both a local and published value defined and grab the needed value
        if not "Local" in config_setting:
            raise Exception(f'The requested property "{name}" does not have a Local value defined.')
        if not "Published" in config_setting:
            raise Exception(f'The requested property "{name}" does not have a Published value defined.')    
        return(config_setting["Published"] if published_version else config_setting["Local"])
