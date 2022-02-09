import java.io.*;
import java.util.*;
public class Program {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		try {
			FileReader fr = new FileReader("ulaz.txt");
			BufferedReader br = new BufferedReader(fr);
			int brKarakteristika = Integer.parseInt(br.readLine().split(" ")[0]);
			
			List<String[]> karakteristike = new ArrayList<String[]>();
			
			for (int i = 0; i < brKarakteristika; i++) {
				karakteristike.add(br.readLine().split(" "));
			}
			
			int brBaza = Integer.parseInt(br.readLine().split(" ")[0]);
			
			List<String[]> baze = new ArrayList<String[]>();
			
			for (int i = 0; i < brBaza; i++) {
				baze.add((br.readLine().split(" ")));
			}
			
			List<String[]> testovi = new ArrayList<String[]>();

			for (int i = 0; i < brBaza; i++) {
				
				if(!sadrzi(testovi,baze.get(i))) {
					testovi.add(baze.get(i));
				}
				for (int j = 0; j < baze.get(i).length; j++) {
					for (int j2 = 0; j2 < Integer.parseInt(karakteristike.get(j)[1]); j2++) {
						if(!karakteristike.get(j)[j2+2].equals(baze.get(i)[j])) {
							String[] newBase = baze.get(i).clone();
							newBase[j]=karakteristike.get(j)[j2+2];
							if(!sadrzi(testovi,newBase)) {
								testovi.add(newBase);
							}
						}
					}
				}
			}
			System.out.println((testovi.size()));
			for (int i = 0; i < testovi.size(); i++) {
				for (int j = 0; j < testovi.get(i).length; j++) {
					System.out.print(testovi.get(i)[j]+" ");
				}
				System.out.println();
			}
			
			File myObj = new File("izlaz.txt");
		      if (myObj.createNewFile()) {
		        System.out.println("File created: " + myObj.getName());
		        
		        FileWriter fw = new FileWriter(myObj);
		        
		        fw.write((testovi.size()+"\n"));
		        for (int i = 0; i < testovi.size(); i++) {
					for (int j = 0; j < testovi.get(i).length; j++) {
						fw.write(testovi.get(i)[j]+" ");
					}
					fw.write("\n");
				}
		        System.out.println("Successfully wrote to the file izlaz.txt.");
		        fw.close();
		      } else {
		        System.out.println("File already exists.");
		        FileWriter fw = new FileWriter("izlaz.txt");
		        
		        fw.write((testovi.size()+"\n"));
		        for (int i = 0; i < testovi.size(); i++) {
					for (int j = 0; j < testovi.get(i).length; j++) {
						fw.write(testovi.get(i)[j]+" ");
					}
					fw.write("\n");
				}
		        System.out.println("Successfully wrote to the file izlaz.txt.");
		        fw.close();
		      }
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		
	}

	private static boolean sadrzi(List<String[]> potencijalniTestovi, String[] newBase) {
		boolean rez = false;
		boolean[] test = new boolean[newBase.length];
		for (int i = 0; i < potencijalniTestovi.size(); i++) {
			for (int t = 0; t < test.length; t++) {
				test[t]=false;
			}
			for (int j = 0; j < potencijalniTestovi.get(i).length; j++) {
				if(potencijalniTestovi.get(i)[j].equals(newBase[j])) {
					test[j]=true;
				}
			}
			boolean t = true;
			for (int j = 0; j < test.length; j++) {
				if(!test[j]) {
					t = false;
				}
			}
			if(t) {
				rez=true;
				break;
			}
		}
		
		return rez;
	}

}
