openocd-x64-0.7.0.exe -f stm32f417vgt6.cfg -c init  -c "reset halt" -c "flash write_image erase MR771.elf" -c "reset run" -c shutdown