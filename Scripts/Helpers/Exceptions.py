"""
Derived exception class for Platform Specific Exceptions.

This exception is thrown when a certain method has a platform specific implementation
and something attempts to call it in a non-platform specific base class.
"""
class PlatformSpecificException(Exception):

    """
    Constructs a new instance of this class
    """
    def __init__(self):
        print("This method must be implemented using platform specific code. " +
            "Provide an implementation in a platform-specific derived class and call the method from there.")
        

"""
Derived exception class for Administrator Permission Required Exceptions.

This exception is thrown when a certain method requires Administrator / Super-User permissions,
however the script isn't currently running with sufficient permissions.
"""
class AdministratorPermissionsRequiredException(Exception):

    """
    Constructs a new instance of this class
    """
    def __init__(self):
        print("This method requires administrator / super-user permissions. " +
              "Please retry this command from a shell with the sufficient permissions.")