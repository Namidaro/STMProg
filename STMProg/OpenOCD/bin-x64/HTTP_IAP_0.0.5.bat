openocd-x64-0.7.0.exe -f stm32f417vgt6.cfg -c init  -c "reset halt" -c "flash write_image erase HTTP_IAP_0.0.5.elf" -c "reset run" -c shutdown