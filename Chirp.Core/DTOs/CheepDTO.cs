using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirp.Core.DTOs;
public record CheepDTO
{
    public required string Message { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public required string AuthorId { get; set; }
    public required string AuthorName { get; set; }
}
