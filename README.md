# C#-Tokenizer

This project is tokenizing/lexing C# codes from .cs files. If the .cs files data is a valid C# code then this program will lexing it.

You are able to use differrent files by adding the filepath to the program. If you want to test how it works then you can use it in default
mode that means you just have to press ENTER and it will show you an example.

The program not just write the result to the screen, but also create a .cs file where you will find it. It is a helpfull feature, because
you are able to read this file with your own program and you can re-create the original C# code from it.

The tokens are on a .CSV file and the program read it after you added a filename or filepath. It is working like a database and you can
easily adding more and more tokens. You are able to modify the .CSV file between tokenizings.

Bugs:
- The program can not handle comments in the code, so in this version you will see them as keywords, separators etc.
