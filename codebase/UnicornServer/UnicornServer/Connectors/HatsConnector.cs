﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using UnicornServer.Models;

namespace UnicornServer.Connectors
{
  /// <summary>
  /// Offers Hats specific database access
  /// Autor: Franziska Haaf
  /// </summary>
  public class HatsConnector
  {
    /// <summary>
    /// The context to access the database
    /// </summary>
    public DatabaseContext Context { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="context">To be injected</param>
    public HatsConnector(DatabaseContext context)
    {
      Context = context;
    }

    /// <summary>
    /// Gets all hats from the DB
    /// </summary>
    /// <returns>A list of Hats</returns>
    public List<Hat> GetAllHats()
    {
      return Context.Hats.ToList();
    }
  }
}