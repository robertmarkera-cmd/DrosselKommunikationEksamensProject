using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PrisPilot.ViewModels
{
    public abstract class BaseViewModel<TEntity> : SuperClassViewModel
    {
        public TEntity Entity { get; }

        public BaseViewModel(TEntity entity)
        {
            Entity = entity;
        }
    }
}
