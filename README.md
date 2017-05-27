# HOW TO GET SETUP
## Visual Studio 2017
Download Visual Studio Community from here: https://www.visualstudio.com/vs/
Install to the default location. When it asks you what options to install, pick:
1. UWP
	1. MAKE SURE EVERY CHECKBOX ON THE SIDE IS CHECKED.
1. Mobile Game Dev C# Xamarin thing that I can't quite remember.
	1. For this one, you can uncheck everything except for Xamarin workbooks and whatever the last thing was.
## Monogame 3.6
Download and install to default location from here: http://www.monogame.net/downloads/
## Nez
This is the library we are using to create our game but we have included it as part of the git repository. When you clone this repo it will already be packaged with the code.
## Cloning the Repo
If you don't know how to do this, just install gitkraken from here: https://www.gitkraken.com/
Once you open gitkraken, click login with your GitHub account and give it access.
Follow the GUI to clone this repo to whatever folder you wish.
## Setting up Visual Studio to compile the code easily
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