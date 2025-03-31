# MusicBookingApp

## Description

This is my template for projects in C#. It has a consistent folder structure and responses for the API. An editorconfig file is also included. The template mixes Clean  Architecture with Vertical Slice Architecture.  

## Installation
Clone the repository  

```bash
git clone https://github.com/henrychris/musicbookingapp.git
```

cd to the root directory  

```bash
cd musicbookingapp
```

Install the template
```bash
dotnet new install .
```

## Usage

Create a new project
```bash
dotnet new musicbookingapp -n YourProjectName
```
You can run the project using the `dotnet run` command in the ProjectName.Host folder. The project will be available at `http://localhost:5051`.
Access `http://localhost:5051/swagger` to see the available endpoints.
