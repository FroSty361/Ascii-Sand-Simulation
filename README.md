# Ascii-Sand-Simulation
A sand simulation console application in ascii made in C#.

# Block Types
1. Empty. Empty space for and to fall, Ascii Value Is " "
2. Sand. Sand block which has gravity, Ascii Value Is *
3. Slime. Once a sand falls on this block, the sand bounces upward, Ascii Value Is =
4. Slope. Left Slope and Right Slope, Pushes the sand to a new column
   Left, Ascii Value Is /
   Right, Ascii Value Is \
5. Teleporter. Once a sand falls on this block, the sand teleports to a corresponding teleporter, Asci Value Is O
6. Solid. Stops the sand from falling, Ascii Value Is #
