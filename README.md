# Battleship
Api for random battleship game between 2 players

Api settings:
url: https://localhost:7176 (you can change it in Battleship/Battleship.Api/Properties/launchSettings.json, section "applicationUrl")

Battleship settings:
Maximum count of tries to arrange ship on grid: 20 (you can change it in Battleship/Battleship.Api/appsettings.json, section "ArrangeShipTries")

Api reference:
GET /Battleship/Get/{id} - gets game by id
GET /Battleship/Get - gets all games
POST /Battleship/CreateGame/{id} - creates game with provided Id
POST /Battleship/ArrangeShips/{id} - arrange ships for game with provided Id
POST /Battleship/NextMove/{id} - makes next move for game with provided Id
