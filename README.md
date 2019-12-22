# Poker Showdown

- The Library was built using .net standard 2.0
- The CLI application was built using .net core 2.2. In order to build, run & test. Please install the .Net Core 2.2 (v2.2.8) SDK at https://dotnet.microsoft.com/download/dotnet-core/2.2.

# Setup
The fastest way to build, test and run the project is by...
1. open your favorite terminal & make sure dotnet sdk is avaiable (i.e. `dotnet --version`; version number should show up)
2. change directory to `<path_to_repo>`
3. execute `dotnet clean` then `dotnet build` to make sure solution builds
4. execute `dotnet test` to run the tests
5. execute `dotnet run --project PokerHandShowdown.Cli` to use the CLI application


# CLI
- The CLI interface will loop through the creation process to add player hands.
  1. First, it will ask you to enter a name of the player
  1. Second, it will ask you to enter the player's hand
     1. The CLI represents a hand something like `Ks-Qh-4c-10d-9s`
        1. each card is delimited by `-`
        1. each card first expects the card value (2 -> 10 or `J` for Jack, `Q` for Queen, `K` for King, `A` for Ace) followed by the suit (`s` for Spade, `h` for Hearts, `c` for Clubs, `d` for Diamond)
  1. Once all the player hands have been entered and the creation loop has been terminated, the showdown will happen and a winner will be displayed
