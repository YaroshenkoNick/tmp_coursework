using System;
using System.Collections.Generic;

namespace goldbach;

public partial class Running
{
    public int Id { get; set; }

    public string Mode { get; set; } = null!;

    public string Input { get; set; } = null!;

    public string Output { get; set; } = null!;

    public DateTime DateTime { get; set; }

}
