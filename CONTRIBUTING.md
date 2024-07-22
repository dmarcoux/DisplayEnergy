# How This Mod Was Created

Refer to the [_SMAPI_ modder
guide](https://stardewvalleywiki.com/Modding:Modder_Guide) to learn how to
create _SMAPI_ mods. This is a simplified version of the guide and without using
any GUI from an IDE like _Visual Studio_.

1. Create the project's directory and go into this directory:

```bash
mkdir DisplayEnergy && cd DisplayEnergy
```

2. Spin up the development environment:

```bash
nix develop
```

3. Create a solution:

```bash
dotnet new sln
```

4. Create a _Class Library_ project and add it to the solution:

```bash
dotnet new classlib -o DisplayEnergy && dotnet sln add DisplayEnergy/DisplayEnergy.csproj
```

5. Delete the _Class1.cs_ or _MyClass.cs_ file (it was generated in the previous step, but it's not needed):

```bash
rm -f DisplayEnergy/Class1.cs DisplayEnergy/MyClass.cs
```

6. Add [_SMAPI_ package](https://smapi.io/package/readme) to allow modding with _SMAPI_:

```bash
dotnet add DisplayEnergy package Pathoschild.Stardew.ModBuildConfig --version 4.0.0
```

7. Create the _ModEntry.cs_ and _manifest.json_ files (see examples in the [_SMAPI_ modder guide](https://stardewvalleywiki.com/Modding:Modder_Guide)):

```bash
touch DisplayEnergy/ModEntry.cs DisplayEnergy/manifest.json
```

# Development Environment

Reproducible development environment for .NET projects which relies on
[Nix](https://github.com/NixOS/nix) [Flakes](https://nixos.wiki/wiki/Flakes),
a purely functional and cross-platform package manager.

**Install Steam and Stardew Valley**

On NixOS, add `programs.steam.enable = true;` to NixOS' configuration.

**Start development environment**

```bash
nix develop
```

**Once inside the development environment...**

_...download and install [SMAPI](https://smapi.io/):_

```bash
curl -O -L https://github.com/Pathoschild/SMAPI/releases/download/4.0.8/SMAPI-4.0.8-installer.zip &&
   unzip SMAPI-4.0.8-installer.zip &&
   steam-run ./SMAPI\ 4.0.8\ installer/install\ on\ Linux.sh &&
   rm -rf "SMAPI 4.0.8 installer" SMAPI-4.0.8-installer.zip
```

_...launch [JetBrains Rider](https://www.jetbrains.com/rider/) or another IDE:_

```bash
# Launch JetBrains Rider
just code
# Launch Visual Studio Code
just code code
```

_...or perhaps execute any of the other [just](https://github.com/casey/just)
recipes/commands included in the [justfile](./justfile):_

```bash
# List all available just recipes
just
```

_...change whatever needs to change, then build and test the mod:_

```bash
just build
# Then launch Stardew Valley, the mod will be installed
```

# How to Release a New Version

1. Update the [semantic version](https://semver.org/) in the [mod manifest](./DisplayEnergy/manifest.json) and push all changes.

2. Create a release with [_GitHub CLI_](https://cli.github.com/): `gh release create VERSION_NUMBER ./DisplayEnergy/bin/Debug/**/*.zip`

3. Download the ZIP archive _DisplayEnergy.VERSION_NUMBER.zip_ from the new release and upload it to [ModDrop](https://www.moddrop.com/stardew-valley/mods/1087175-displayenergy) / [NexusMods](https://www.nexusmods.com/stardewvalley/mods/10662).
