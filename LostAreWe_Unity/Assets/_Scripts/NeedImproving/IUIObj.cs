using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIObj {

    // Name of UI Object for reference.
    string Name { get; set; }

    void Setup();
}
