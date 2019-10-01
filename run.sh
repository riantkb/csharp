#!/bin/bash
mono tools/ConcatSources.exe Program.cs "$@" > submittable.txt
if [ $? -ne 0 ] ; then
    echo "Exit due to Concatenation Error." >&2
    exit
fi
mcs submittable.txt -out:Program.exe
if [ $? -ne 0 ] ; then
    echo "Exit due to Compilation Error." >&2
    exit
fi
mono Program.exe < /dev/stdin
# cat submittable.txt | pbcopy
