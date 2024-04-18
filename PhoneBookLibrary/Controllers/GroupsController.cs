﻿using PhoneBookLibrary.Models;
using Spectre.Console;

namespace PhoneBookLibrary.Controllers;

public class GroupsController
{
  public static Group? GetGroupById(int groupId)
  {
    using PhoneBookContext db = new();

    Group? group = db.Groups.FirstOrDefault(group => group.GroupId == groupId);

    if (group == null)
    {
      AnsiConsole.Markup("[red]Group with given ID doesn't exists. [/]\n");
      return null;
    }

    return group;
  }

  public static List<Group>? GetGroups()
  {
    using PhoneBookContext db = new();

    if (db.Groups.ToList().Count == 0)
    {
      AnsiConsole.Markup("[red]Groups list is empty.[/] Create one first. ");
      return null;
    }

    return [.. db.Groups];
  }

  public static void InsertGroup(string groupName)
  {
    Group group = new() { Name = groupName };

    using PhoneBookContext db = new();
    db.Add(group);
    int stateChanges = db.SaveChanges();

    if (stateChanges == 0) AnsiConsole.Markup("[red]Group adding failed. [/]");
    else AnsiConsole.Markup("[green]Group added! [/]");
  }

  public static void UpdateGroup(Group group)
  {
    using PhoneBookContext db = new();

    db.Update(group);
    int stateChanges = db.SaveChanges();

    if (stateChanges == 0) AnsiConsole.Markup("[red]Group updating failed. [/]");
    else AnsiConsole.Markup("[green]Group updated! [/]");
  }
}
