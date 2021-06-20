# Last Testament of Wanderers 
# Copyright (C) 2019 - 2021 ALiwoto
# This file is subject to the terms and conditions defined in
# file 'LICENSE', which is part of the source code.

# clear the screen (the terminal)
clear

echo -e "bulding it, please wait a bit..."

# build the solution (*.sln file)
dotnet build

# clear the screen (the terminal)
clear

echo -e "we are done building it,\n now running the game...\n-------------------"

# finally, run the executable file which is in debug directory
LTW/bin/Debug/net5.0/linux-x64/LTW

