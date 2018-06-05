openocd-0.7.0.exe -f LPC.cfg -c "init"  -c "reset halt" -c "flash write_image erase D:/projects/ARM/iAccess_LPC1820/Release/iAccess_LPC1820.elf" -c "reset run" -c "shutdown"


