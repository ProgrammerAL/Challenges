using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using AsyncAwaitBestPractices;

using FileAndDirectoryBrowserWebApp.ViewModels;

using Microsoft.AspNetCore.Components;

namespace FileAndDirectoryBrowserWebApp.Components
{
    public class DirectoryComponentBase : ComponentBase, IDisposable
    {
        private DirectoryViewModel? _viewModel;
        private string? _path;

        [Inject]
        [NotNull]
        public DirectoryViewModel? ViewModel
        {
            get => _viewModel!;
            set
            {
                if (_viewModel != value)
                {
                    UnhookViewModelEvents();
                    _viewModel = value;
                    HookUpViewModelEvents();
                    LoadFromPath();
                }
            }
        }

        [Parameter]
        public string? Path
        {
            get => _path;
            set
            {
                _path = value;
                LoadFromPath();
            }
        }

        public void Dispose()
        {
            UnhookViewModelEvents();
        }

        private void LoadFromPath()
        {
            ViewModel.LoadAsync(_path).SafeFireAndForget();
        }

        private void HookUpViewModelEvents()
        {
            if (_viewModel is object)
            {
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void UnhookViewModelEvents()
        {
            if (_viewModel is object)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
        }

        private async void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}
