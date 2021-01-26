using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class SectionOutlet : IComponent, IDisposable
    {
        private static RenderFragment EmptyRenderFragment = builder => { };
        private string _subscribedName;
        private SectionRegistry _registry;
        private Action<RenderFragment> _onChangeCallback;

        public void Attach(RenderHandle renderHandle)
        {
            _onChangeCallback = content => renderHandle.Render(content ?? EmptyRenderFragment);
            _registry = SectionRegistry.GetRegistry(renderHandle);
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            var suppliedName = parameters.GetValueOrDefault<string>("Name");

            if (suppliedName != _subscribedName)
            {
                _registry.Unsubscribe(_subscribedName, _onChangeCallback);
                _registry.Subscribe(suppliedName, _onChangeCallback);
                _subscribedName = suppliedName;
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _registry?.Unsubscribe(_subscribedName, _onChangeCallback);
        }
    }
}
