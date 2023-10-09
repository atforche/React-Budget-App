from .CommonFunctions import CommonFunctions
import ctypes
from .Exceptions import AdministratorPermissionsRequiredException
import subprocess

"""
Derived class containing helper functions that require Windows specific code
"""
class WindowsFunctions(CommonFunctions):
    
    """
    Constructs a new instance of this class

    Args:
        base_repository_path: name of the base repository path where the application code lives
    """
    def __init__(self, base_repository_path: str):
        super().__init__(base_repository_path)


    """
    Verifies that the process is currently running with administrator/super-user privileges
    """
    def verify_running_as_admin(self):
        if not ctypes.windll.shell32.IsUserAnAdmin() != 0:
            raise AdministratorPermissionsRequiredException()
        

    """
    Checks whether the service currently exists.

    Args:
        published_version: True to check for the published service, false to check for the local service.

    Returns:
        True if the service exists, false otherwise
    """
    def does_service_exist(self, published_version: bool) -> bool:
        service_name: str = self.get_config_setting("RestApiServiceName", published_version)
        command: str = f"Get-Service -Name '{service_name}' -ErrorAction SilentlyContinue"
        return len(subprocess.run(["powershell.exe", "-Command", command], capture_output=True).stdout) > 0

    """
    Creates an individual application service.

    Args:
        published_version: If True, create the published service. Otherwise, create the local service.
    """
    def create_service(self, published_version: bool):
        service_name: str = self.get_config_setting("RestApiServiceName", published_version)
        rest_api_exe: str = self.get_config_setting("AppDirectory", published_version) + "/bin/RestApi/RestApi.exe"
        rest_api_url: str = self.get_config_setting("RestApiUrl", published_version)
        rest_api_port: int = self.get_config_setting("RestApiPort", published_version)
        command: str = "New-Service " + \
            f"-Name '{service_name}' " + \
            f"-BinaryPathName ('{rest_api_exe} --urls={rest_api_url}:{rest_api_port}') " + \
            f"-DisplayName '{service_name}' " + \
            "-Description \"REST API for the React Budget App\" " + \
            "-StartupType Manual"
        subprocess.run(["powershell.exe", "-Command", command], check=True)
    

    """
    Stops an individual application service.

    Args:
        published_version: If True, stop the published service. Otherwise, stop the local service.
    """
    def stop_service(self, published_version: bool):
        service_name: str = self.get_config_setting("RestApiServiceName", published_version)
        command: str = f"Stop-Service -Name '{service_name}'"
        subprocess.run(["powershell.exe", "-Command", command], check=True)
    

    """
    Starts an individual application service.

    Args:
        published_version: If True, start the published service. Otherwise, start the local service.
    """
    def start_service(self, published_version: bool):
        service_name: str = self.get_config_setting("RestApiServiceName", published_version)
        command: str = f"Start-Service -Name '{service_name}'"
        subprocess.run(["powershell.exe", "-Command", command], check=True)