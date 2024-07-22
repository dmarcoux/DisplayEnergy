# Documentation: https://github.com/casey/just

# Default recipe will list all just recipes in the order they appear in this justfile
default:
  just --list --unsorted

[doc("Launch IDE, but allow its process to live even if the development environment gets killed")]
code ide="rider":
  nohup {{ide}} &
  @# The shell seems to be stuck after the execution of the previous command with `nohup`. Pressing `Enter` shows the
  @# shell prompt back, but we don't need to do anything anyway, so we can simply get the shell prompt back with `exit`
  @exit

[doc("Build the solution")]
build:
  dotnet build

[doc("Generate global.json after manually changing the .NET SDK package in `flake.nix` or updating `flake.lock`")]
generateGlobalJson:
  @# The .NET SDKs have been updated when manually changing the packages in `flake.nix` or by recreating `flake.lock` to update
  @# the revision of every input to its current revision with `nix flake update`.
  @#
  # Without removing global.json, the dotnet CLI won't execute commands due to a mismatch in the .NET SDK version. So global.json has to be removed first.
  rm --force global.json
  dotnet new globaljson # TODO .NET 6: --roll-forward disable
  # Mention documentation in comments at the top of global.json
  sed -i -e '1s|^|// Documentation: https://learn.microsoft.com/en-us/dotnet/core/tools/global-json\n// Comments are supported in this JSON file. Refer to the documentation above\n|' global.json
