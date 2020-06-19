# C#-Tokenizer

This project tokenizes C# codes from .cs files. If the data of the .cs files is a valid C# code then this program tokenizes it.

You are able to use differrent files by adding a filepath to the program. If you want to test how it works then you can use it in default
mode that means you just have to press ENTER and it shows you an example.

The program not just writes the result to the screen, but also creates a .cs file where you will find it. It is a helpful feature, because
you are able to read this file with your own program and you can re-create the original C# code from it.

The tokens are on a .CSV file and the program reads it after adding a filename or filepath. It works like a database and you can
easily add more and more tokens. You are able to modify the .CSV files between different tokenizations.

Bugs:
- The program can not handle comments in the code, so in this version you will see them as keywords etc.
