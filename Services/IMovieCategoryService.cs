namespace Backend.Services
{
    public interface IMovieCategoryService 
    {
        public Task AddMovieCategory(int movie_id, int category_id);
        public Task RemoveMovieCategory(int movieCategory_id);
    }
}
