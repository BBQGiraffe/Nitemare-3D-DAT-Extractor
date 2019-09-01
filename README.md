# Nitemare3D DAT Extractor
extracts Menu art and sounds from the 1994 FPS game Nitemare 3D

## Usage
launch the Executable and click "choose output directory" to choose where you want your files to go,
click "Select DAT file" and navigate to your Nitemare 3D folder,
open either SND.DAT or UIF.DAT,
SND.DAT will give you Midi music and Voc or PCM sound files, UIF.DAT will give you Pcx image files(can be opened with Photoshop)

these files will be extracted to C:\Nitemare

## compiling
I have included the Visual Studio project files,
you could probably port this to Linux easily(only Windows code is the GUI)

## project website
https://bbqgiraffegames.com


## issues
sound for the Windows version comes out as VOC but is actually PCM, working on fixing.
