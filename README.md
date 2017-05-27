# Dimensional Beats

## Story and Mechanics and Other Links
[See General](GENERAL.md)

## DEVELOPMENT
### Pulling and Pushing from GitHub
For the love of god, PLEASE stash your own changes before pulling... it makes merge errors so much nicer.
#### GitKraken
1. Click on your WIP in the home view of GitKraken.
2. Click stash on the top.
3. Click pull on the top.
4. Click pop on the top.
5. Deal with any remaining merge errors.
#### Terminal or Git Shell or Powershell or Windows Subsystem for Linux (WSL) or whatever have you
`git stash save && git pull --rebase && git stash pop`
### Coding Standards
Just follow how the rest of the code looks.. most of it follows C#'s usual coding standards. E.g. Brackets are on separate lines (I know it sucks; that's how C# likes to do their brackets).

## HOW TO GET SETUP
### Visual Studio 2017
Download Visual Studio Community from here: [Visual Studio](https://www.visualstudio.com/vs/)
Install to the default location. When it asks you what options to install, pick:
1. UWP
	1. On the side there is a `Summary` pane.
	1. Expand `Universal Windows Platform` and you can uncheck everything from Optional if you so wish.
1. Mobile Development with .NET
	1. For this one, follow the same instructions as UWP except expand `Mobile Development with .NET`.
	1. You only need Xamarin Workbooks and Java SE Development Kit.
### Monogame 3.6
Download and install to default location from here: [MonoGame 3.6](http://www.monogame.net/downloads/)
### Nez
This is the library we are using to create our game but we have included it as part of the git repository. When you clone this repo it will already be packaged with the code.
### Cloning the Repo
If you don't know how to do this, just install gitkraken from here: [GitKraken](https://www.gitkraken.com/)
Once you open gitkraken, click login with your GitHub account and give it access.
Follow the GUI to clone this repo to whatever folder you wish.
### Setting up Visual Studio to compile the code easily
After installation, find where you cloned the Github Repository and navigate to
`DimensionalBeats/DimensionalBeats/DimensionalBeats.csproj`
Open it with Visual Studio 2017.
Now follow these steps completely or you will not be able to compile the game.
1. In the Solution Explorer (probably on the right side) click on `Solution 'DimensionalBeats' (2 Projects)`.
2. With it highlighted, on the top left of the window click on `File->Save DimensionalBeats.sln`.
3. It should prompt you to find a place to save it. Save it in the root of `{Whatever folder you cloned this repo into}/DimensionalBeats`. It should be in a directory where there is another folder called `packages`.
4. Now go back to the Solution Explorer and expand "Dimensional Beats" if it is not expanded already. Expand `References` and right click `Nez`. Hit Remove.
5. Right Click `References` and click `Add Reference`. On the left pane of the new window, click Projects and select Nez.
6. Now in the Solution Explorer once more, collapse the `DimensionalBeats` project and expand `Nez (Portable)`.
7. Expand `References`. Right click `Monogame.Framework` and remove it as well.
8. Right Click `References` and hit add Reference.
9. In the left pane, select Browse and browse for `C:/Program Files (x86)/MonoGame/v3.0/Assemblies/DesktopGL/Monogame.Framework.dll`.
10. Add it as a reference and now right click `Nez (Portable)` and hit Build.
11. The game should finally build! Hit the play button on the top.
## Complete List of Libraries and Credits for Assets
1. Monogame 3.6
1. Nez forked for Monogame 3.6 created by a certain `Robert Willis`.
1. David created most prototype assets.