# DIKUGames
Repo for solving assignments with DIKUArcade for DIKU course Software Development

To start working on the assignments, create a **fork** of this repository
and follow the instructions in the assignment description.


### Run
```dotnet run --project Galaga```


### New concepts in A7
This week's assignment employs a few new concepts that we have not used (much) before.
- [The null-coalescing operator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator)
- [The singleton pattern (creational pattern)](https://en.wikipedia.org/wiki/Singleton_pattern#:~:text=In%20software%20engineering%2C%20the%20singleton,class%20to%20a%20singular%20instance.)
- [State machines](https://www.freecodecamp.org/news/state-machines-basics-of-computer-science-d42855debc66/)



### If you accidentally format DIKUArcade
Some bugs will occur in DIKUArcade if you run 
`dotnet format .` \
The reason these errors happen is that we are formatting .NET 6,
but DIKUArcade uses .NET 5. 
Instead, just format the Galaga project:
```dotnet format Galaga```

If you accidentally format the entire project, and you don't know
the git commands to fix it, you can use these commands:
```bash
sudo rm -r DIKUArcade
mkdir DIKUArcade
git submodule update --init --recursive
dotnet clean
```

This removes DIKUArcade from the project and clones it again.

If you're using the handout VM, the sudo password is empty. 
Just press enter when prompted.