public class Mutant {
	public static long funk (long ar, long ar2, int k, boolean e) {
		long sum = 0;
		long[] pi = new long[]{1,2,3,6,7};
		long[] j = new long[]{5,4,3,2,1};
		for(int i = 0; i <= k; ++i) {
			if(e) sum = sum + ar - pi[ /* <- mutant */ i];
			sum = sum * ar2 + pi[i];
}
		return sum;
}
public static void main(String[] args){

try{
		System.out.println(Mutant.funk(1573l, 376l, 3, false));
} catch(Exception e36){
		System.out.println("There was an exception " + e36.getMessage());
}
}
}