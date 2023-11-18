using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Common.Models;
public class AppSettings
{ 
   

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }

}
