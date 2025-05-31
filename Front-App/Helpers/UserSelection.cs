using API.Migrations;
using API.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_App.Helpers;

public static class UserSelection
{
    public static Shift? CreateShift()
    {
        string name = AnsiConsole.Prompt(new TextPrompt<string>("[Blue]Insert employee name[/]")
            );
        string start = AnsiConsole.Prompt(new TextPrompt<string>("[Blue]Insert shift's start date[/]")
            .Validate(s =>
            {
                return ValidateDate(s);
            }));
        DateTime startDate = DateTime.ParseExact(start, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        string end = AnsiConsole.Prompt(new TextPrompt<string>("[Blue]Insert shift's end date[/]")
            .Validate(s =>
            {
                return ValidateDate(s);
            }));
        DateTime endDate = DateTime.ParseExact(end, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        if (!IsDatesValid(startDate, endDate))
        {
            AnsiConsole.MarkupLine("[Red]End date before starting date or shift too long![/]");
            return null;
        }
        return new Shift()
        {
            EmployeeName = name,
            Start = startDate,
            End = endDate,
            Duration = endDate - startDate
        };
    }

    private static ValidationResult ValidateDate(string s)
    {
        if (!DateTime.TryParseExact(s, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture,
                  DateTimeStyles.None, out var parsed))
        {
            return ValidationResult.Error("Use dd.mm.yyyy HH:mm format");
        }
        if (parsed >= DateTime.Now)
        {
            return ValidationResult.Error("Can't Use a date from the future as a starting date");
        }
        return ValidationResult.Success();
    }

    private static bool IsDatesValid(DateTime start, DateTime end) => start < end && (end - start) < TimeSpan.FromHours(24);
}