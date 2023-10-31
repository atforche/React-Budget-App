from .CommonFunctions import CommonFunctions
from .Exceptions import AdministratorPermissionsRequiredException
import os

"""
Derived class containing helper functions that require Linux specific code
"""
class LinuxFunctions(CommonFunctions):

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
        if not os.getuid() == 0:
            raise AdministratorPermissionsRequiredException()
        

    """
    Checks whether the service currently exists.

    Args:
        published_version: True to check for the published service, false to check for the test service.

    Returns:
        True if the service exists, false otherwise
    """
    def does_service_exist(self, published_version: bool) -> bool:
        raise NotImplemented()
    

    """
    Creates an individual application service.

    Args:
        published_version: If True, create the published service. Otherwise, create the test service.
    """
    def create_service(self, published_version: bool):
        raise NotImplemented()
    

    """
    Stops an individual application service.

    Args:
        published_version: If True, stop the published service. Otherwise, stop the test service.
    """
    def stop_service(self, published_version: bool):
        raise NotImplemented()
    

    """
    Starts an individual application service.

    Args:
        published_version: If True, start the published service. Otherwise, start the test service.
    """
    def start_service(self, published_version: bool):
        raise NotImplemented()