emu8086 supports additional devices that can be created by anyone.

there are 7 example devices:

  Simple.exe
  LED_Display.exe
  Thermometer.exe
  Printer.exe                (written by Andrew Nelis)
  Robot.exe
  Stepper_motor.exe
  Thermometer.exe
  Traffic_Lights.exe  

source code for these devices can be found in c:\emu8086\DEVELOPER
these devices are programmed in visual basic; other languages (java, borland turbo c, c#, delphi...)
also can be used - all you have to do is to get access to this file: c:\emu8086.io

to write your own device using visual basic, all you have to do is to include io.bas
module into your project, and use pre-made subs and functions
 (look inside that file for more information).

    for visual basic 6.0 include io.bas
    for visual basic .net include io.vb
    for c/c++ include io.h
    for visual c# include io.cs           (written by deTrox Yang)

avaiable input / output addresses for custom devices are from 0 to 65535 (0 to 0FFFFh) 

port 100 corresponds to byte 100 in this file c:\emu8086.io, port 101 to the byte 101, port 0 to byte 0 etc...


if new devices are put into this folder: c:\emu8086\devices, they are automatically added
to virtual devices menu of the emulator when it starts.
device is activated when its file name is found anywhere in comments or in string buffers.


feel free to e-mail if you have any questions: info@emu8086.com


