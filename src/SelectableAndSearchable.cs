namespace NP.Concepts.Behaviors
{
    public class SelectableAndSearchable<T> : SelectableItem<T>, ISearchable
        where T : SelectableAndSearchable<T>
    {
        public string SearchableStr { get; private set; }

        string _originalSearchableStr;

        public string OriginalSearchableStr
        {
            get => _originalSearchableStr;
            protected set
            {
                if (_originalSearchableStr == value)
                    return;

                _originalSearchableStr = value;

                SearchableStr = _originalSearchableStr?.ToLower();
            }
        }

        #region SearchStr Property
        private string _searchStr;
        public string SearchStr
        {
            get
            {
                return this._searchStr;
            }
            set
            {
                if (this._searchStr == value)
                {
                    return;
                }

                this._searchStr = value?.ToLower();
                this.OnPropertyChanged(nameof(SearchStr));

                this.OnPropertyChanged(nameof(IsFoundBySearch));
            }
        }
        #endregion SearchStr Property

        public bool IsFoundBySearch =>
            this.SearchStrFound();
    }
}
