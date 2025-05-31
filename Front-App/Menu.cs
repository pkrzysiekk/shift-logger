using Front_App.Helpers;
using Front_App.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_App;

public class Menu
{
    private readonly IShiftClient _shiftApiClient;

    public Menu(IShiftClient shiftClient)
    {
        _shiftApiClient = shiftClient;
    }

    private MenuChoices GetUserMenuChoice()
    {
        var selection = AnsiConsole.Prompt(new SelectionPrompt<MenuChoices>()
            .Title("What would you want to do?")
            .AddChoices(Enum.GetValues<MenuChoices>())
            .UseConverter(choice => choice.GetDescription())
            );
        return selection;
    }

    public async Task ShowMenu()
    {
        var selection = GetUserMenuChoice();
        while (selection != MenuChoices.Exit)
        {
            switch (selection)
            {
                case MenuChoices.ShowShifts:
                    var shifts = await _shiftApiClient.GetShiftsAsync();
                    AnsiConsole.MarkupLine("[green]Showing all shifts...[/]");
                    DataVisualizer.DisplayShifts(shifts);
                    break;

                case MenuChoices.AddShift:
                    var shift = UserSelection.CreateShift();
                    while (shift == null)
                        shift = UserSelection.CreateShift();
                    try
                    {
                        await _shiftApiClient.CreateShiftAsync(shift);
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.MarkupLine($"[red] {ex}[/]");
                    }
                    AnsiConsole.MarkupLine("[green]Adding a new shift...[/]");
                    break;

                case MenuChoices.EditShift:
                    // Logic to edit an existing shift
                    AnsiConsole.MarkupLine("[green]Editing an existing shift...[/]");
                    break;

                case MenuChoices.DeleteShift:
                    // Logic to delete a shift
                    AnsiConsole.MarkupLine("[green]Deleting a shift...[/]");
                    break;
            }
            selection = GetUserMenuChoice();
            AnsiConsole.Clear();
        }
    }
}