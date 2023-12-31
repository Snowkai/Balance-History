﻿using Balance_History.Models;
using Balance_History.src;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Balance_History.ViewModels
{
    public partial class RecordViewModel : ObservableObject
    {
        private readonly DatabaseContex _context;
        public RecordViewModel(DatabaseContex context)
        {
            _context = context;
        }
        [ObservableProperty]
        private ObservableCollection<Record> _records = new();

        [ObservableProperty]
        private Record _operatingRecord = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        public async Task LoadRecordsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var records = await _context.GetAllAsync<Record>();
                if (records is not null && records.Any())
                {
                    if (records is null)
                    {
                        Records ??= new ObservableCollection<Record>();
                    }
                    foreach (var record in records)
                    {
                        Records.Add(record);
                    }
                }
            }, "Fetching records...");
        }

        [RelayCommand]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        private void SetOperatingRecord(Record? record) => OperatingRecord = record ?? new();
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        [RelayCommand]
        private async Task SaveRecordAsync()
        {
            if (OperatingRecord is null)
            {
                //return;
                //SetOperatingRecord(new());
            }
            var (IsValied, ErrorMessage) = OperatingRecord.Validate();
            if (!IsValied)
            {
                await Shell.Current.DisplayAlert("Validation Error", ErrorMessage, "Ok");
                return;
            }
            var busyText = OperatingRecord.Id == 0 ? "Creating record..." : "Updating record";
            await ExecuteAsync(async () =>
            {
                if (OperatingRecord.Id == 0)
                {
                    //Create record
                    await _context.AddItemAsync<Record>(OperatingRecord);
                    Records.Add(OperatingRecord);
                }
                else
                {
                    //update record
                    await _context.UpdateItemAsync<Record>(OperatingRecord);
                    var recordCopy = OperatingRecord.Clone();
                    var index = Records.IndexOf(OperatingRecord);
                    Records.RemoveAt(index);
                    Records.Insert(index, recordCopy);
                }
                SetOperatingRecordCommand.Execute(new());
            }, busyText);
        }
        [RelayCommand]
        private async Task DeleteRecordAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByKeyAsync<Record>(id))
                {
                    var record = Records.FirstOrDefault(x => x.Id == id);
                    Records.Remove(record);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete error", "Record was not  deleted", "Ok");
                }
            }, "Deleting record...");
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex) { }
#pragma warning restore CS0168 // Variable is declared but never used
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }
    }
}
