# ShipItApp

## Features
- **Email Support** — prompt, validate, and store email for each user
- **Password Security (M8)** — salted hashing (`salt:hash`) in `users.csv` (no plain passwords)
- **Validation (M8)** — password must be **≥ 8 characters** and include **at least one letter and one digit**
- **Users Menu** — list users with passwords hidden
- **Console UX** — colored headers, prompts, and friendly messages

## How to Run (.NET 8)
```bash
dotnet run -p ShipItApp.csproj


## Usage
1. 'cd (Your project path)dev/src/ShipItApp'
2. `dotnet restore`  
3. `dotnet run`  
4. Create or log in to a user (password required), then explore the menu.

## Extra
1. 'added sln file manually'
2. 'Added password validations'

   ****Important: The application was built in .NET 8.0.****
## Final Milestone (M8)

### What changed
- Stronger password validation and friendly messages
- Input trimming and safer console output (no plain passwords)
- Polished CLI flow (clear menus, Back/Exit)
- Screenshots/GIF for quick review

### Build & Run (.NET 8)
```bash
dotnet build
dotnet run
