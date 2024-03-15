namespace ProductService.Common.Exceptions;

public class NotFoundException:  Exception
{
    public NotFoundException():base("Not found")
    {
        
    }
}