﻿using CloudStoage.Domain;
using CloudStorage.Applications.EventHandle;
using CloudStorage.Domain.Shared;
using System.Collections.Concurrent;
using System.Drawing;
using Token.EventBus;

namespace CloudStorage.Layou.Components.Uploads
{
    partial class UploadTheList
    {
        [Inject]
        public UploadingEventBus UploadingEventBus { get; set; }

        [Inject]
        public IKeyLocalEventBus<Tuple<long, Guid>> UploadTheListEventBus { get; set; }

        public static BlockingCollection<UploadingDto> UploadingList { get; set; }

        protected override async void OnInitialized()
        {

            UploadingList = UploadingEventBus.UploadingList;


            await UploadTheListEventBus.Subscribe(KeyLoadNames.UploadingListName, a =>
            {
                foreach (var d in UploadingList)
                {
                    if (d.Id == a.Item2)
                    {
                        d.UploadingSize = a.Item1;
                        StateHasChanged();
                        return;
                    }
                }

            });

        }
    }
}
