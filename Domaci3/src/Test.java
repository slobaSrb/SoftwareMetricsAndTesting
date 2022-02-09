import java.util.Iterator;

public class Test { 

	public static int sum(int[] x, int a)  
    {  

      int s = 0;  

      for (int i=0; i < x.length; i++)  
      {  

              s = s + x[i] + a;   
              s = s - x[i] - a;
              s = s * x[i] * a;   
              s = s / x[i] / a;
              s += x[i];   
              s -= x[i];
              s *= x[i];   
              s /= x[i];
              i++;
              i--;
              // s = s - x[i];  

      }  

      return s;  

    } 
    
    public static void main(String[] args) { 
        System.out.println("Start");
        int[] x = new int[args.length-1];
        for (int i = 0; i < args.length-1; i++) {
			x[i] = Integer.parseInt(args[i]);
		}
        int a = Integer.parseInt(args[args.length-1]);
        System.out.println(Test.sum(x,a));
    } 
 
}