using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ngordat.net.backend.transversal.Logs
{
  /// <summary>
  /// LogHelper class.
  /// </summary>
  public static class LogHelper
  {
    /// <summary>
    /// Serializes an object into a string (with properties).
    /// </summary>
    /// <param name="obj">The object to convert to string.</param>
    /// <returns>The string containing all object properties.</returns>
    public static string GetObjectToString(object obj)
    {
      // Get all properties and value of the object.
      var properties = from property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                       select new
                       {
                         Name = property.Name,
                         Value = property.GetValue(obj, null)
                       };

      StringBuilder sb = new StringBuilder();
      // Add the type of the object.
      sb.AppendLine(obj.GetType().ToString());

      // And each of it's properties.
      foreach (var property in properties)
        sb.AppendLine($"{property.Name}:{property.Value}");

      return sb.ToString();
    }
  }
}
