using Native;

class Program
{
    static void Main(string[] args)
    {
        // Display the number of command line arguments:
        for(int i=0;i<args.Length;i++){
          System.Console.WriteLine("Converting file: " + args[0] + " to midi");
        }
        open_gp_file file;
        //if(args.Length == 1){
          file = new open_gp_file(args[0], "");
        /*}
        if(args.Length == 2){
          file = new open_gp_file(args[0], args[1]);
        }
        else{
          System.Console.WriteLine("BLEAAAAAH" + args.Length);
          return;
        }*/
        file.OutputRoutine();
    }
}
