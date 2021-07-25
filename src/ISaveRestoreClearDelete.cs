namespace NP.Concepts.Behaviors
{
    public interface ISaveRestoreClearDelete
    {
        // added Self to the end
        // to avoid clashes with other methods

        string SaveSelf();

        void LoadSelf(string serialized);

        void ClearSelf();

        //void DeleteSelf();
    }
}
