# DIKUGames
Repo for solving assignments with DIKUArcade for DIKU course Software Development

To start working on the assignments, create a **fork** of this repository
and follow the instructions in the assignment description.


### Run
```dotnet run --project Galaga```


### Format
```dotnet format . -v diag --report Galaga```


### If you accidentally format DIKUArcade
Some bugs will occur in DIKUArcade if you run 
`dotnet format . -v diag --report .`. \
The reason these errors happen is that we are formatting .NET 6,
but DIKUArcade uses .NET 5. \
Instead of formatting the entire project, just format Galaga:
```bash
dotnet format . -v diag --report Galaga
```

If you accidentally format the entire project, and you don't know
the git commands to fix it, you can use these commands:
```bash
rm -r DIKUArcade
mkdir DIKUArcade
git submodule update --init --recursive
```