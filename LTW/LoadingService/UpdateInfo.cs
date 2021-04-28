using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW.LoadingService
{
    [Serializable]
    public class UpdateInfo
    {
        private GameVersion latestVersion;
        private int updateElementsCount;
        private string updateListPath;
        private string releasedNotes;
        private string releasedDate;

        public UpdateInfo() : this(GameVersion.BasicVersion, 0, "notSet", "notSet", "2020 / 8 / 23")
        {

        }
        public UpdateInfo(GameVersion LatestVerValue, int updateElementsCountValue, 
            string updateListPathValue, string releasedNotesValue, string releasedDateValue)
        {
            LatestVersion = LatestVerValue;
            UpdateElementsCount = updateElementsCountValue;
            UpdateListPath = updateListPathValue;
            ReleasedNotes = releasedNotesValue;
            ReleasedDate = releasedDateValue;
        }
        public GameVersion LatestVersion
        {
            get
            {
                return latestVersion;
            }
            set
            {
                if(value == null)
                {
                    latestVersion = GameVersion.BasicVersion;
                }
                else
                {
                    latestVersion = value;
                }
            }
        }
        public int UpdateElementsCount
        {
            get
            {
                return updateElementsCount;
            }
            set
            {
                if(value >= 0)
                {
                    updateElementsCount = value;
                }
                else
                {
                    updateElementsCount = 0;
                }
            }
        }
        public string UpdateListPath
        {
            get
            {
                return updateListPath;
            }
            set
            {
                if(value == null)
                {
                    updateListPath = "notSet";
                }
                else
                {
                    updateListPath = value;
                }
            }
        }
        public string ReleasedNotes
        {
            get
            {
                return releasedNotes;
            }
            set
            {
                if (value == null)
                {
                    releasedNotes = "notSet";
                }
                else
                {
                    releasedNotes = value;
                }
            }
        }
        public string ReleasedDate
        {
            get
            {
                return releasedDate;
            }
            set
            {
                if (value == null)
                {
                    releasedDate = "notSet";
                }
                else
                {
                    releasedDate = value;
                }
            }
        }
    }
}
