﻿using Adic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardGameRoot : ContextRoot
{
    public override void Init()
    {
    }

    public override void SetupContainers()
    {
        //Add
        var container = this.AddContainer<InjectionContainer>();

        //Register any extensions the container may use.
        container.RegisterExtension<UnityBindingContainerExtension>()
                .Bind<GameTracker>().ToSingleton()
                .Bind<CardAssetsProvider>().ToSingleton();

        //Bind a Transform component to the two cubes on the scene, using a "As" condition
        //to define their identifiers.
        //.Bind<GameTracker>().ToGameObject("LeftCube").As("LeftCube")
        //.Bind<Transform>().ToGameObject("RightCube").As("RightCube")
        //Bind the "GameObjectRotator" component to a new game object of the same name.

        //This component will then receive the reference to the "LeftCube", making only
        //this cube rotate.
        //.Bind<GameObjectRotator>().ToGameObject();
    }
}