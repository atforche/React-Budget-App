import argparse
import platform
import os
from Helpers.LinuxFunctions import LinuxFunctions
from Helpers.WindowsFunctions import WindowsFunctions

# Base path of the Git repository
BASE_REPOSITORY_PATH = os.path.abspath(os.path.dirname(__file__) + "\\..")

"""
Parses the command line input and runs the appropriate command.
"""
def main():
    # Move to the base repository directory
    os.chdir(BASE_REPOSITORY_PATH)

    # Initialize the argument parser
    parser = argparse.ArgumentParser(
        prog="Common-Functions.py",
        description="Budget App Utility Scripts")
    parser.add_argument('command', 
                        choices=["Deploy", "New-Migration", "Remove-Migration", "Get-Config"], 
                        help="Command for the script to run")
    parser.add_argument('--name',
                        help="Name of the config setting to grab or the migration to create/delete",
                        required=False)
    parser.add_argument('--published',
                        help="If set, deploys the published version of the application or " + 
                            "gets the published value for the configuration setting.",
                        required=False,
                        action='store_true')
    parser.add_argument('--remove-all',
                        help="If set, all migrations will be removed from the Schema directory",
                        required=False,
                        action='store_true')
    
    # Grab the correct helper class for the given platform
    if (platform.uname().system == "Windows"):
        helper = WindowsFunctions(BASE_REPOSITORY_PATH)
    else:
        helper = LinuxFunctions(BASE_REPOSITORY_PATH)
    
    # Parse the arguments and dispatch the correct command
    args = parser.parse_args()
    if args.command == "Deploy":
        helper.deploy_version(args.published)
    elif args.command == "New-Migration":
        if args.name is None:
            parser.error("The New-Migration command requires the name argument to be provided")
        helper.create_new_migration(args.name)
    elif args.command == "Remove-Migration":
        helper.remove_migration(args.remove_all)    
    elif args.command == "Get-Config":
        if args.name is None:
            parser.error("The Get-Config command requires the name argument to be provided")
        print(helper.get_config_setting(args.name, args.published))    


# Main entry point for script
if __name__ == "__main__":
    main()