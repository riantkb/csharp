#!/bin/bash
mono tools/ConcatSources.exe Program.cs "$@" > submittable.txt
if [ $? -ne 0 ] ; then
    echo "Exit due to Concatenation Error." >&2
    exit
fi
mcs submittable.txt -out:Program.exe >&2
if [ $? -ne 0 ] ; then
    echo "Exit due to Compilation Error." >&2
    exit
fi
echo "Compilation succeeded." >&2
mono Program.exe < /dev/stdin
# cat submittable.txt | pbcopy
