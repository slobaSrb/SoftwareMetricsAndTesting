import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map.Entry;
import java.util.Scanner;
import java.util.Set;
import java.util.TreeMap;
import java.util.concurrent.TimeUnit;

public class CompileRunJavaProgram {
	static TreeMap<Integer, ArrayList<String>> listaRezultata = new TreeMap<Integer, ArrayList<String>>();
	static TreeMap<String, ArrayList<String>> naziviPromenljivihIstogTipa = new TreeMap<String, ArrayList<String>>();
	static TreeMap<String, ArrayList<String>> vrednostiPromenljivih = new TreeMap<String, ArrayList<String>>();
	static List<String> sviMutanti = new ArrayList<String>();
	static int test = 1;

	public static void main(String[] args) {
		String separator = File.separator;
		// System.out.println("**********");
		// String command = "javac src/Test.java";
		// runProcess(command);
		// System.out.println("**********");
		List<String> mutantiVarList = new ArrayList<String>();
		try {
			List<String> data = new ArrayList<String>();
			try {
				File myObj = new File("src/ulazniTestPrimer.txt");
				Scanner myReader = new Scanner(myObj);
				int counter = -1;
				while (myReader.hasNextLine()) {
					String test = myReader.nextLine();
					if (test.contains("test")) {
						data.add("");
						counter++;
						continue;
					}
					String firstStr = test;
					int equalSign = firstStr.indexOf("=");
					String value = firstStr.substring(equalSign).trim();
					if (value.contains("new")) {
						int indBracket = value.indexOf("{");
						value = value.substring(indBracket, value.length() - 1).trim();
					}

					if (data.get(counter) != "")
						data.set(counter, data.get(counter) + " " + value);
					else
						data.set(counter, value);

				}
				// command = "java src/Test.java " + data;
				// runProcess(command);
				// myReader.close();
			} catch (FileNotFoundException e) {
				System.out.println("An error occurred.");
				e.printStackTrace();
			}
			
			try {
				File myObj1 = new File("src/Test.java");
				File myObjUlaznaFunkc = new File("src/ulaz.txt");
				Scanner myReader1 = new Scanner(myObj1);
				Scanner myReaderUlaznaFunkc = new Scanner(myObjUlaznaFunkc);

				String methodSigning = myReaderUlaznaFunkc.nextLine();
				int indOfFirstBracket = methodSigning.indexOf("(");
				String[] methodWords = methodSigning.substring(0, indOfFirstBracket).split(" ");
				String methodName = methodWords[methodWords.length - 1];
				String kodZaPromenu = methodSigning;
				boolean pocetak = false, metoda = false;
				int zagrade = 0;
				Scanner pomScan;
				String data1;
				while (myReader1.hasNextLine()) {
//					if(!pocetak) {
//					data = myReader1.nextLine();
//					}
//					else {
					data1 = myReaderUlaznaFunkc.nextLine();
//					}
//					if (data.contains("sum(")) {
//						pocetak = true;
//						data = methodSigning;
//					}
//					if (pocetak) {
					if (data1.trim().length() > 1 && !data1.trim().substring(0, 2).equals("//")) {
						kodZaPromenu += data1 + "\n";
					}
					if (data1.contains("{")) {
						zagrade += 1;
						if (data1.trim().equals("{"))
							kodZaPromenu += "{\n";
					}

					if (data1.contains("}")) {
						zagrade -= 1;
						if (data1.trim().equals("}"))
							kodZaPromenu += "}\n";
					}
					if (zagrade > 0) {
						metoda = true;
					}
					if (metoda & zagrade == 0) {
						break;
					}
//					}
				}

				List<String> aritmetika = new ArrayList<String>();
				aritmetika.add("+ ");
				aritmetika.add("- ");
				aritmetika.add("* ");
				aritmetika.add("/ ");
				aritmetika.add("% ");

				List<String> mutantiPlusList = new ArrayList<String>();
				if (kodZaPromenu.contains("+ ")) {
					String[] SplitMutantiPlus = kodZaPromenu.trim().split("\\+ ");
					String[] mutantiPlus = new String[(SplitMutantiPlus.length - 1) * 4];
					if (SplitMutantiPlus.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika.size(); j2++) {
							for (int i = 0; i < SplitMutantiPlus.length - 1; i++) {
								mutantiPlus[brojac] = "";
								for (int j = 0; j < SplitMutantiPlus.length; j++) {
									if (!aritmetika.get(j2).equals("+ ") & j != i) {
										if (j < SplitMutantiPlus.length - 1) {
											mutantiPlus[brojac] += SplitMutantiPlus[j] + "+ ";
										} else {
											mutantiPlus[brojac] += SplitMutantiPlus[j];
											mutantiPlusList.add(mutantiPlus[brojac]);
											brojac++;
										}
									} else if (!aritmetika.get(j2).equals("+ ") & j == i) {
										mutantiPlus[brojac] += SplitMutantiPlus[j] + aritmetika.get(j2) + " /* <- mutant */ ";
									}
								}
							}
						}
					}
				}

				List<String> mutantiMinusList = new ArrayList<String>();
				String[] mutantiMinus;
				if (kodZaPromenu.contains("- ")) {
					String[] SplitMutantiMinus = kodZaPromenu.split("- ");
					mutantiMinus = new String[(SplitMutantiMinus.length) * 4];
					if (SplitMutantiMinus.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika.size(); j2++) {
							for (int i = 0; i < SplitMutantiMinus.length - 1; i++) {
								mutantiMinus[brojac] = "";
								for (int j = 0; j < SplitMutantiMinus.length; j++) {
									if (!aritmetika.get(j2).equals("- ") & j != i) {
										if (j < SplitMutantiMinus.length - 1) {
											mutantiMinus[brojac] += SplitMutantiMinus[j] + "- ";
										} else {
											mutantiMinus[brojac] += SplitMutantiMinus[j];
											mutantiMinusList.add(mutantiMinus[brojac]);
											brojac++;
										}
									} else if (!aritmetika.get(j2).equals("- ") & j == i) {
										mutantiMinus[brojac] += SplitMutantiMinus[j] + aritmetika.get(j2) + " /* <- mutant */ ";
									}
								}
							}
						}
					}
				}

				List<String> mutantiPutaList = new ArrayList<String>();
				String[] mutantiPuta;
				if (kodZaPromenu.contains("* ")) {
					String[] SplitMutantiPuta = kodZaPromenu.split("\\* ");
					mutantiPuta = new String[(SplitMutantiPuta.length) * 4];
					if (SplitMutantiPuta.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika.size(); j2++) {
							for (int i = 0; i < SplitMutantiPuta.length - 1; i++) {
								mutantiPuta[brojac] = "";
								for (int j = 0; j < SplitMutantiPuta.length; j++) {
									if (!aritmetika.get(j2).equals("* ") & j != i) {
										if (j < SplitMutantiPuta.length - 1) {
											mutantiPuta[brojac] += SplitMutantiPuta[j] + "* ";
										} else {
											mutantiPuta[brojac] += SplitMutantiPuta[j];
											mutantiPutaList.add(mutantiPuta[brojac]);
											brojac++;
										}
									} else if (!aritmetika.get(j2).equals("* ") & j == i) {
										mutantiPuta[brojac] += SplitMutantiPuta[j] + aritmetika.get(j2) + " /* <- mutant */ ";
									}
								}
							}
						}
					}
				}

				List<String> mutantiPodeljenoList = new ArrayList<String>();
				String[] mutantiPodeljeno;
				if (kodZaPromenu.contains("/ ")) {
					String[] SplitMutantiPodeljeno = kodZaPromenu.split("/ ");
					mutantiPodeljeno = new String[(SplitMutantiPodeljeno.length) * 4];
					if (SplitMutantiPodeljeno.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika.size(); j2++) {
							for (int i = 0; i < SplitMutantiPodeljeno.length - 1; i++) {
								mutantiPodeljeno[brojac] = "";
								for (int j = 0; j < SplitMutantiPodeljeno.length; j++) {
									if (!aritmetika.get(j2).equals("/ ") & j != i) {
										if (j < SplitMutantiPodeljeno.length - 1) {
											mutantiPodeljeno[brojac] += SplitMutantiPodeljeno[j] + "/ ";
										} else {
											mutantiPodeljeno[brojac] += SplitMutantiPodeljeno[j];
											mutantiPodeljenoList.add(mutantiPodeljeno[brojac]);
											brojac++;
										}
									} else if (!aritmetika.get(j2).equals("/ ") & j == i) {
										mutantiPodeljeno[brojac] += SplitMutantiPodeljeno[j] + aritmetika.get(j2) + " /* <- mutant */ ";
									}
								}
							}
						}
					}
				}

				List<String> mutantiModuoList = new ArrayList<String>();
				String[] mutantiModuo;
				if (kodZaPromenu.contains("% ")) {
					String[] SplitMutantiModuo = kodZaPromenu.split("% ");
					mutantiModuo = new String[(SplitMutantiModuo.length) * 4];
					if (SplitMutantiModuo.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika.size(); j2++) {
							for (int i = 0; i < SplitMutantiModuo.length - 1; i++) {
								mutantiModuo[brojac] = "";
								for (int j = 0; j < SplitMutantiModuo.length; j++) {
									if (!aritmetika.get(j2).equals("% ") & j != i) {
										if (j < SplitMutantiModuo.length - 1) {
											mutantiModuo[brojac] += SplitMutantiModuo[j] + "% ";
										} else {
											mutantiModuo[brojac] += SplitMutantiModuo[j];
											mutantiModuoList.add(mutantiModuo[brojac]);
											brojac++;
										}
									} else if (!aritmetika.get(j2).equals("% ") & j == i) {
										mutantiModuo[brojac] += SplitMutantiModuo[j] + aritmetika.get(j2) + " /* <- mutant */ ";
									}
								}
							}
						}
					}
				}

				List<String> aritmetika2 = new ArrayList<String>();
				aritmetika2.add("+=");
				aritmetika2.add("-=");
				aritmetika2.add("*=");
				aritmetika2.add("/=");

				List<String> mutantiPlusJednakoList = new ArrayList<String>();
				String[] mutantiPlusJednako;
				if (kodZaPromenu.contains("+=")) {
					String[] SplitMutantiPlusJednako = kodZaPromenu.split("\\+=");
					mutantiPlusJednako = new String[(SplitMutantiPlusJednako.length) * 4];
					if (SplitMutantiPlusJednako.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika2.size(); j2++) {
							for (int i = 0; i < SplitMutantiPlusJednako.length - 1; i++) {
								mutantiPlusJednako[brojac] = "";
								for (int j = 0; j < SplitMutantiPlusJednako.length; j++) {
									if (!aritmetika2.get(j2).equals("+=") & j != i) {
										if (j < SplitMutantiPlusJednako.length - 1) {
											mutantiPlusJednako[brojac] += SplitMutantiPlusJednako[j] + "+=";
										} else {
											mutantiPlusJednako[brojac] += SplitMutantiPlusJednako[j];
											mutantiPlusJednakoList.add(mutantiPlusJednako[brojac]);
											brojac++;
										}
									} else if (!aritmetika2.get(j2).equals("+=") & j == i) {
										mutantiPlusJednako[brojac] += SplitMutantiPlusJednako[j] + aritmetika2.get(j2);
									}
								}
							}
						}
					}
				}

				List<String> mutantiMinusJednakoList = new ArrayList<String>();
				String[] mutantiMinusJednako;
				if (kodZaPromenu.contains("-=")) {
					String[] SplitMutantiMinusJednako = kodZaPromenu.split("-=");
					mutantiMinusJednako = new String[(SplitMutantiMinusJednako.length) * 4];
					if (SplitMutantiMinusJednako.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika2.size(); j2++) {
							for (int i = 0; i < SplitMutantiMinusJednako.length - 1; i++) {
								mutantiMinusJednako[brojac] = "";
								for (int j = 0; j < SplitMutantiMinusJednako.length; j++) {
									if (!aritmetika2.get(j2).equals("-=") & j != i) {
										if (j < SplitMutantiMinusJednako.length - 1) {
											mutantiMinusJednako[brojac] += SplitMutantiMinusJednako[j] + "-=";
										} else {
											mutantiMinusJednako[brojac] += SplitMutantiMinusJednako[j];
											mutantiMinusJednakoList.add(mutantiMinusJednako[brojac]);
											brojac++;
										}
									} else if (!aritmetika2.get(j2).equals("-=") & j == i) {
										mutantiMinusJednako[brojac] += SplitMutantiMinusJednako[j]
												+ aritmetika2.get(j2);
									}
								}
							}
						}
					}
				}

				List<String> mutantiPutaJednakoList = new ArrayList<String>();
				String[] mutantiPutaJednako;
				if (kodZaPromenu.contains("*=")) {
					String[] SplitMutantiPutaJednako = kodZaPromenu.split("\\*=");
					mutantiPutaJednako = new String[(SplitMutantiPutaJednako.length) * 4];
					if (SplitMutantiPutaJednako.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika2.size(); j2++) {
							for (int i = 0; i < SplitMutantiPutaJednako.length - 1; i++) {
								mutantiPutaJednako[brojac] = "";
								for (int j = 0; j < SplitMutantiPutaJednako.length; j++) {
									if (!aritmetika2.get(j2).equals("*=") & j != i) {
										if (j < SplitMutantiPutaJednako.length - 1) {
											mutantiPutaJednako[brojac] += SplitMutantiPutaJednako[j] + "*=";
										} else {
											mutantiPutaJednako[brojac] += SplitMutantiPutaJednako[j];
											mutantiPutaJednakoList.add(mutantiPutaJednako[brojac]);
											brojac++;
										}
									} else if (!aritmetika2.get(j2).equals("*=") & j == i) {
										mutantiPutaJednako[brojac] += SplitMutantiPutaJednako[j] + aritmetika2.get(j2);
									}
								}
							}
						}
					}
				}

				List<String> mutantiPodeljenoJednakoList = new ArrayList<String>();
				String[] mutantiPodeljenoJednako;
				if (kodZaPromenu.contains("/=")) {
					String[] SplitMutantiPodeljenoJednako = kodZaPromenu.split("/=");
					mutantiPodeljenoJednako = new String[(SplitMutantiPodeljenoJednako.length) * 4];
					if (SplitMutantiPodeljenoJednako.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika2.size(); j2++) {
							for (int i = 0; i < SplitMutantiPodeljenoJednako.length - 1; i++) {
								mutantiPodeljenoJednako[brojac] = "";
								for (int j = 0; j < SplitMutantiPodeljenoJednako.length; j++) {
									if (!aritmetika2.get(j2).equals("/=") & j != i) {
										if (j < SplitMutantiPodeljenoJednako.length - 1) {
											mutantiPodeljenoJednako[brojac] += SplitMutantiPodeljenoJednako[j] + "/=";
										} else {
											mutantiPodeljenoJednako[brojac] += SplitMutantiPodeljenoJednako[j];
											mutantiPodeljenoJednakoList.add(mutantiPodeljenoJednako[brojac]);
											brojac++;
										}
									} else if (!aritmetika2.get(j2).equals("/=") & j == i) {
										mutantiPodeljenoJednako[brojac] += SplitMutantiPodeljenoJednako[j]
												+ aritmetika2.get(j2);
									}
								}
							}
						}
					}
				}

				List<String> aritmetika1 = new ArrayList<String>();
				aritmetika1.add("++");
				aritmetika1.add("--");

				List<String> mutantiPlusPlusList = new ArrayList<String>();
				String[] mutantiPlusPlus;
				if (kodZaPromenu.contains("++")) {
					String[] SplitMutantiPlusPlus = kodZaPromenu.split("\\+\\+");
					mutantiPlusPlus = new String[(SplitMutantiPlusPlus.length) * 4];
					if (SplitMutantiPlusPlus.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika1.size(); j2++) {
							for (int i = 0; i < SplitMutantiPlusPlus.length - 1; i++) {
								mutantiPlusPlus[brojac] = "";
								for (int j = 0; j < SplitMutantiPlusPlus.length; j++) {
									if (!aritmetika1.get(j2).equals("++") & j != i) {
										if (j < SplitMutantiPlusPlus.length - 1) {
											mutantiPlusPlus[brojac] += SplitMutantiPlusPlus[j] + "++";
										} else {
											mutantiPlusPlus[brojac] += SplitMutantiPlusPlus[j];
											mutantiPlusPlusList.add(mutantiPlusPlus[brojac]);
											brojac++;
										}
									} else if (!aritmetika1.get(j2).equals("++") & j == i) {
										mutantiPlusPlus[brojac] += SplitMutantiPlusPlus[j] + "--";
									}
								}
							}
						}
					}
				}

				List<String> mutantiMinusMinusList = new ArrayList<String>();
				String[] mutantiMinusMinus;
				if (kodZaPromenu.contains("--")) {
					String[] SplitMutantiMinusMinus = kodZaPromenu.split("--");
					mutantiMinusMinus = new String[(SplitMutantiMinusMinus.length) * 4];
					if (SplitMutantiMinusMinus.length > 1) {
						int brojac = 0;
						for (int j2 = 0; j2 < aritmetika1.size(); j2++) {
							for (int i = 0; i < SplitMutantiMinusMinus.length - 1; i++) {
								mutantiMinusMinus[brojac] = "";
								for (int j = 0; j < SplitMutantiMinusMinus.length; j++) {
									if (!aritmetika1.get(j2).equals("++") & j != i) {
										if (j < SplitMutantiMinusMinus.length - 1) {
											mutantiMinusMinus[brojac] += SplitMutantiMinusMinus[j] + "--";
										} else {
											mutantiMinusMinus[brojac] += SplitMutantiMinusMinus[j];
											mutantiMinusMinusList.add(mutantiMinusMinus[brojac]);
											brojac++;
										}
									} else if (!aritmetika1.get(j2).equals("++") & j == i) {
										mutantiMinusMinus[brojac] += SplitMutantiMinusMinus[j] + "++";
									}
								}
							}
						}
					}
				}

				try {

					File ulazniTestPrimer = new File("src/ulazniTestPrimer.txt");
					Scanner primerReader = new Scanner(ulazniTestPrimer);
					findValues(vrednostiPromenljivih, primerReader);

					File ulaz = new File("src/ulaz.txt");
					Scanner ulazReader = new Scanner(ulaz);
					findVariables(naziviPromenljivihIstogTipa, ulazReader);

					String[] mutantiVar;
					List<String> after = new ArrayList<String>();
					after.add(")");
					after.add("]");
					after.add(" ");
					after.add(";");
					after.add("+");
					after.add("-");
					after.add("/");
					after.add("*");
					after.add("%");
					after.add("=");
					
					List<String> before = new ArrayList<String>();
					before.add(";");
					before.add("+");
					before.add("-");
					before.add("/");
					before.add("*");
					before.add("%");
					before.add(" ");
					before.add("(");

					for (Entry<String, ArrayList<String>> entry1 : naziviPromenljivihIstogTipa.entrySet()) {
						ArrayList<String> varijable = entry1.getValue();
						for (int i1 = 0; i1 < after.size(); i1++) {
							for (int k = 0; k < varijable.size(); k++) {
								if (!varijable.get(k).contains("[")) {
									varijable.set(k, (String) varijable.get(k) + after.get(i1));

									if (kodZaPromenu.contains(varijable.get(k))) {
										if (varijable.get(k).substring(varijable.get(k).length() - 1).equals(")")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 1) + "\\"
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										} else if (varijable.get(k).substring(varijable.get(k).length() - 1)
												.equals("]")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 1) + "\\"
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										} else if(varijable.get(k).substring(varijable.get(k).length() - 1)
												.equals("+")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 1) + "\\"
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										}
										String[] SplitMutantiVar = kodZaPromenu.split(varijable.get(k));
										if (varijable.get(k).substring(varijable.get(k).length() - 1).equals(")")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 2)
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										} else if (varijable.get(k).substring(varijable.get(k).length() - 1)
												.equals("]")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 2)
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										} else if(varijable.get(k).substring(varijable.get(k).length() - 1)
												.equals("+")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 2)
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										}
										mutantiVar = new String[(SplitMutantiVar.length) * (varijable.size())];
										if (varijable.get(k).length() == 1) {
											varijable.set(k, (String) varijable.get(k).subSequence(0,
													varijable.get(k).length() - 1));
											break;
										}
										if (SplitMutantiVar.length > 1) {
											int brojac = 0;
											for (int j2 = 0; j2 < varijable.size(); j2++) {
												for (int i = 0; i < SplitMutantiVar.length - 1; i++) {
													mutantiVar[brojac] = "";
													for (int j = 0; j < SplitMutantiVar.length; j++) {
														if (!varijable.get(j2).equals(varijable.get(k)) & j != i) {
															if (j < SplitMutantiVar.length - 1) {
																mutantiVar[brojac] += SplitMutantiVar[j]
																		+ varijable.get(k);
															} else {
																mutantiVar[brojac] += SplitMutantiVar[j];
																mutantiVarList.add(mutantiVar[brojac]);
																brojac++;
															}
														} else if (!varijable.get(j2).equals(varijable.get(k))
																& j == i) {
															mutantiVar[brojac] += SplitMutantiVar[j] + varijable.get(j2) + " /* <- mutant */ "
																	+ after.get(i1);
														}
													}
												}
											}
										}
									}
									varijable.set(k,
											(String) varijable.get(k).subSequence(0, varijable.get(k).length() - 1));
								}
							}
						}
					}
					for (Entry<String, ArrayList<String>> entry1 : naziviPromenljivihIstogTipa.entrySet()) {
						ArrayList<String> varijable = entry1.getValue();
						for (int i1 = 0; i1 < before.size(); i1++) {
							for (int k = 0; k < varijable.size(); k++) {

								if (kodZaPromenu.contains(before.get(i1) + varijable.get(k))) {
									if (varijable.get(k).contains("[")) {
										varijable.set(k,
												(String) varijable.get(k).subSequence(0, varijable.get(k).length() - 1)
														+ "\\[");
										String[] SplitMutantiVar = kodZaPromenu
												.split(before.get(i1) + varijable.get(k));
										if (varijable.get(k).substring(varijable.get(k).length() - 1).equals(")")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 2)
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										} else if (varijable.get(k).substring(varijable.get(k).length() - 1)
												.equals("]")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 2)
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										}
										mutantiVar = new String[(SplitMutantiVar.length) * (varijable.size())];
										if (varijable.get(k).length() == 1) {
											varijable.set(k, (String) varijable.get(k).subSequence(0,
													varijable.get(k).length() - 2) + "[");
											break;
										}
										if (SplitMutantiVar.length > 1) {
											int brojac = 0;
											for (int j2 = 0; j2 < varijable.size(); j2++) {
												for (int i = 0; i < SplitMutantiVar.length - 1; i++) {
													mutantiVar[brojac] = "";
													for (int j = 0; j < SplitMutantiVar.length; j++) {
														if (!varijable.get(j2).equals(varijable.get(k)) & j != i) {
															if (j < SplitMutantiVar.length - 1) {
																mutantiVar[brojac] += SplitMutantiVar[j] + before.get(i1)
																		+ varijable.get(k).subSequence(0, varijable.get(k).length() - 2)
																		+ "[";
															} else {
																mutantiVar[brojac] += SplitMutantiVar[j];
																mutantiVarList.add(mutantiVar[brojac]);
																brojac++;
															}
														} else if (!varijable.get(j2).equals(varijable.get(k))
																& j == i) {
															mutantiVar[brojac] += SplitMutantiVar[j] + before.get(i1)
																	+ varijable.get(j2) + " /* <- mutant */ ";
														}
													}
												}
											}
										}
										varijable.set(k,
												(String) varijable.get(k).subSequence(0, varijable.get(k).length() - 2)
														+ "[");
										
									}
									
									
									if(kodZaPromenu.contains(varijable.get(k).subSequence(0,varijable.get(k).length() - 1) + ".")){
										varijable.set(k,
												(String) varijable.get(k).subSequence(0, varijable.get(k).length() - 1)
														+ "\\.");
										String[] SplitMutantiVar1 = kodZaPromenu
												.split(before.get(i1) + varijable.get(k));
										if (varijable.get(k).substring(varijable.get(k).length() - 1).equals(")")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 2)
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										} else if (varijable.get(k).substring(varijable.get(k).length() - 1)
												.equals("]")) {
											varijable.set(k,
													varijable.get(k).substring(0, varijable.get(k).length() - 2)
															+ varijable.get(k)
																	.substring(varijable.get(k).length() - 1));
										}
										mutantiVar = new String[(SplitMutantiVar1.length) * (varijable.size())];
										if (varijable.get(k).length() == 1) {
											varijable.set(k, (String) varijable.get(k).subSequence(0,
													varijable.get(k).length() - 2) + "[");
											break;
										}
										if (SplitMutantiVar1.length > 1) {
											int brojac = 0;
											for (int j2 = 0; j2 < varijable.size(); j2++) {
												for (int i = 0; i < SplitMutantiVar1.length - 1; i++) {
													mutantiVar[brojac] = "";
													for (int j = 0; j < SplitMutantiVar1.length; j++) {
														if (!varijable.get(j2).equals(varijable.get(k)) & j != i) {
															if (j < SplitMutantiVar1.length - 1) {
																mutantiVar[brojac] += SplitMutantiVar1[j] + before.get(i1)
																		+ varijable.get(k).subSequence(0, varijable.get(k).length() - 2) + "[";
															} else {
																mutantiVar[brojac] += SplitMutantiVar1[j];
																mutantiVarList.add(mutantiVar[brojac]);
																brojac++;
															}
														} else if (!varijable.get(j2).equals(varijable.get(k))
																& j == i) {
															mutantiVar[brojac] += SplitMutantiVar1[j] + before.get(i1)
																	+ varijable.get(j2).subSequence(0, varijable.get(j2).length() - 1) + " /* <- mutant */ "
																	+ ".";
														}
													}
												}
											}
										}
										varijable.set(k,(String) varijable.get(k).subSequence(0, varijable.get(k).length() - 2) + "[");
									}
								}
							}
						}
					}
//						}

					// ####################

//						for (int k = 0; k < varijable.size(); k++) {
//							if (!varijable.get(k).contains("[")) {
//								varijable.set(k, (String) " " + varijable.get(k));
//							}
//							if (kodZaPromenu.contains(varijable.get(k) + "")) {
//								if (varijable.get(k).contains("[")) {
//									varijable.set(k,
//											(String) varijable.get(k).subSequence(0, varijable.get(k).length() - 1)
//													+ "\\[");
//								}
//								String[] SplitMutantiVar = kodZaPromenu.split(varijable.get(k));
//								if (varijable.get(k).contains("[")) {
//									varijable.set(k,
//											(String) varijable.get(k).subSequence(0, varijable.get(k).length() - 2)
//													+ "[");
//								}
//								mutantiVar = new String[(SplitMutantiVar.length) * (varijable.size() - 1)];
//
//								if (SplitMutantiVar.length > 1) {
//									int brojac = 0;
//									for (int j2 = 0; j2 < varijable.size(); j2++) {
//										for (int i = 0; i < SplitMutantiVar.length - 1; i++) {
//											if (mutantiVar.length == 0) {
//												break;
//											}
//											mutantiVar[brojac] = "";
//											for (int j = 0; j < SplitMutantiVar.length; j++) {
//												if (!varijable.get(j2).equals(varijable.get(k)) & j != i) {
//													if (j < SplitMutantiVar.length - 1) {
//														mutantiVar[brojac] += SplitMutantiVar[j] + varijable.get(k);
//													} else {
//														mutantiVar[brojac] += SplitMutantiVar[j];
//														mutantiVarList.add(mutantiVar[brojac]);
//														brojac++;
//													}
//												} else if (!varijable.get(j2).equals(varijable.get(k)) & j == i) {
//													mutantiVar[brojac] += SplitMutantiVar[j] + " " + varijable.get(j2);
//												}
//											}
//										}
//									}
//								}
//							}
//							if (!varijable.get(k).contains("[")) {
//								varijable.set(k, (String) varijable.get(k).subSequence(1, varijable.get(k).length()));
//							}
//
//						}

//					}

				} catch (FileNotFoundException e) {
					System.out.println("An error occurred.");
					e.printStackTrace();
				}
				File ulazMute = new File("src/ulaz.txt");
				Scanner scan = new Scanner(ulazMute);
				String potpis = scan.nextLine().trim();
				scan.close();
				for (int i = 0; i < mutantiVarList.size(); i++) {

					if (!mutantiVarList.get(i).contains(potpis)) {
						mutantiVarList.remove(i);
						i--;
					}

				}
				for (int i = 0; i < mutantiVarList.size() - 1; i++) {
					for (int j = i + 1; j < mutantiVarList.size(); j++) {
						if (mutantiVarList.get(i).toString().equals(mutantiVarList.get(j).toString())) {
							mutantiVarList.remove(i);
							i--;
							j = i + 1;
							break;
						}
					}
				}

				System.out.println("ende");

				for (int i = 0; i < mutantiPlusList.size(); i++) {
					sviMutanti.add(mutantiPlusList.get(i));
				}
				for (int i = 0; i < mutantiMinusList.size(); i++) {
					sviMutanti.add(mutantiMinusList.get(i));
				}
				for (int i = 0; i < mutantiPutaList.size(); i++) {
					sviMutanti.add(mutantiPutaList.get(i));
				}
				for (int i = 0; i < mutantiPodeljenoList.size(); i++) {
					sviMutanti.add(mutantiPodeljenoList.get(i));
				}
				for (int i = 0; i < mutantiModuoList.size(); i++) {
					sviMutanti.add(mutantiModuoList.get(i));
				}

				List<String> orgKod = new ArrayList<String>();
				orgKod.add(kodZaPromenu);

				try {
					File myObj = new File("src/TestMutant.java");
					if (myObj.createNewFile()) {
						System.out.println("File created: " + myObj.getName());
						writeToFileAndExecute(myObj, mutantiVarList, methodName, vrednostiPromenljivih);
					} else {
						System.out.println("File already exists.");
						writeToFileAndExecute(myObj, mutantiVarList, methodName, vrednostiPromenljivih);
					}
				} catch (IOException e) {
					System.out.println("An error occurred.");
					e.printStackTrace();
				}
				for (int i = 0; i < mutantiVarList.size(); i++) {
					sviMutanti.add(mutantiVarList.get(i));
				}

				try {
					File writeFile = new File("src/Mutanti.java");
					if (writeFile.createNewFile()) {
						System.out.println("File created: " + writeFile.getName());
						for (Entry<String, ArrayList<String>> entry : vrednostiPromenljivih.entrySet()) {
							writeToFileAndExecuteJavaVMorg(writeFile, orgKod, methodName, entry.getValue());
							writeToFileAndExecuteJavaVM(writeFile, sviMutanti, methodName, entry.getValue());
							test++;
						}
					} else {
						System.out.println("File already exists.");
						for (Entry<String, ArrayList<String>> entry : vrednostiPromenljivih.entrySet()) {
							writeToFileAndExecuteJavaVMorg(writeFile, orgKod, methodName, entry.getValue());
							writeToFileAndExecuteJavaVM(writeFile, sviMutanti, methodName, entry.getValue());
							test++;
						}
					}
				} catch (IOException e) {
					System.out.println("An error occurred.");
					e.printStackTrace();
				}

				try {
					File myObj = new File("src/izlaz.txt");
					if (myObj.createNewFile()) {
						System.out.println("File created: " + myObj.getName());
						FileWriter izlaz = new FileWriter("src/izlaz.txt");
						izlaz.write("");
						for (int i = 0; i < sviMutanti.size(); i++) {
							String mutantMetoda = sviMutanti.get(i).replaceFirst(methodName,
									methodName + "Mutant" + (i + 1));
							izlaz.write("\nM" + (i + 1) + ":\n");
							izlaz.write(mutantMetoda);
						}
						izlaz.flush();
						izlaz.close();
					} else {
						System.out.println("File already exists.");
						FileWriter izlaz = new FileWriter("src/izlaz.txt");
						izlaz.write("");
						for (int i = 0; i < sviMutanti.size(); i++) {
							String mutantMetoda = sviMutanti.get(i).replaceFirst(methodName,
									methodName + "Mutant" + (i + 1));
							izlaz.write("\nM" + (i + 1) + ":\n");
							izlaz.write(mutantMetoda);
						}
						izlaz.flush();
						izlaz.close();
					}
				} catch (IOException e) {
					System.out.println("An error occurred.");
					e.printStackTrace();
				}

//				List<String> naziviMetoda = new ArrayList<String>();
//				try {
//					FileWriter klasaSvihMutanata = new FileWriter("src/Mutanti.java");
//					klasaSvihMutanata.write("");
//					try {
//						File myObj2 = new File("src/Test.java");
//						Scanner myReader2 = new Scanner(myObj2);
//						String kodZaUpis = "";
//						boolean ubaciMutanta = false, ubacenPrefix = false, ubacenPostFix = false;
//						metoda = false;
//						zagrade = 0;
//						while (myReader2.hasNextLine()) {
//							data = myReader2.nextLine();
//
//							if (data.contains("sum(")) {
//								ubaciMutanta = true;
//							}
//							if (!ubaciMutanta) {
//								/*
//								 * if (data.contains("{")) { zagrade += 1; if (data.trim().indexOf("{") == 0) {
//								 * kodZaUpis += "{" + data + "\n"; } else if (data.trim().indexOf("{") ==
//								 * data.trim().length() - 1) { kodZaUpis += data + "{\n"; } } else if
//								 * (data.contains("}")) { zagrade -= 1; if (data.trim().indexOf("}") == 0) {
//								 * kodZaUpis += "}" + data + "\n"; } else if (data.trim().indexOf("}") ==
//								 * data.trim().length() - 1) { kodZaUpis += data + "}\n"; } } else {
//								 */
//								kodZaUpis += data + "\n";
////							}
//							}
//							if (ubaciMutanta) {
//								int zagradeMutant = 0;
//								if (data.contains("{"))
//									zagradeMutant++;
//								while (true) {
//									data = myReader2.nextLine();
//									if (data.contains("{")) {
//										zagradeMutant++;
//									} else if (data.contains("}")) {
//										zagradeMutant--;
//									}
//									if (zagradeMutant == 0) {
//										break;
//									}
//								}
//								kodZaUpis = kodZaUpis.replaceFirst("Test", "Mutanti");
//								klasaSvihMutanata.write(kodZaUpis);
//								klasaSvihMutanata.write(kodZaPromenu);
//								naziviMetoda.add(methodName);
//								for (int i = 0; i < sviMutanti.size(); i++) {
//									String mutantMetoda = sviMutanti.get(i).replaceFirst(methodName,
//											methodName + "Mutant" + (i + 1));
//									naziviMetoda.add(methodName + "Mutant" + (i + 1));
//									klasaSvihMutanata.write(mutantMetoda);
//								}
//								ubacenPrefix = true;
//								ubaciMutanta = false;
//							}
//							klasaSvihMutanata.flush();
//							boolean wroteToMain = false;
//							while (myReader2.hasNextLine() && ubacenPrefix) {
//								data = myReader2.nextLine();
//								if (data.contains("main(")) {
//									klasaSvihMutanata.write(data);
//									klasaSvihMutanata.write("\t\tSystem.out.println(\"Start\");\n");
//									for (int i = 0; i < naziviMetoda.size(); i++) {
////										if(i==0) {klasaSvihMutanata.write("\ntry{"+
////												"\n\t\tMutanti." + naziviMetoda.get(0) + "("
////												+ poredjajPromenljive(vrednostiPromenljivih) + ");" + "\n"
////												+"} catch(Exception e){\n\t\tSystem.out.println(\"There was an exception ORIGINALNI KOD VAM NIJE DOBAR\");\n}");
////									
////										
////										}
//										klasaSvihMutanata.write("\ntry{"+
//												"\n\t\tSystem.out.println(" + "Mutanti." + naziviMetoda.get(i) + "("
//														+ poredjajPromenljive(vrednostiPromenljivih) + "));" + "\n"
//												+"} catch(Exception e"+ i +"){\n\t\tSystem.out.println(\"There was an exception \" + e"+i+".getMessage());\n}");
//										wroteToMain = true;
//									}
//									klasaSvihMutanata.flush();
//								} else {
//									if (!wroteToMain) {
//										klasaSvihMutanata.write(data + "\n");
//										klasaSvihMutanata.flush();
//									}
//								}
//								if (wroteToMain) {
//									klasaSvihMutanata.write("}\n}\n");
//									ubacenPostFix = true;
//									break;
//								}
////							}
//							}
//							if (zagrade > 0) {
//								metoda = true;
//							}
//							if (ubacenPostFix) {
//								break;
//							}
//
//						}
//
//						myReader1.close();
//
//					} catch (FileNotFoundException e) {
//						System.out.println("An error occurred.");
//						e.printStackTrace();
//					}
//					klasaSvihMutanata.flush();
//					klasaSvihMutanata.close();
//					System.out.println("kraj");
//					String separator1 = File.separator;
//					System.out.println("Start Compiling **********");
//					String command = "javac src/Mutanti.java";
//					runProcess(command);
//					System.out.println("Compiled **********");
//					System.out.println("Start Exec **********");
//					try {
//					String commandMutants = "java src/Mutanti.java";
//					runProcess(commandMutants);
//					} catch(Exception e) {
//						e.printStackTrace();
//					}
//					System.out.println("Executed **********");
//					
//				} catch (Exception e) {
//					System.out.println(e);
//				}

				try {
					List<Boolean> slabo = new ArrayList<Boolean>();
					List<Boolean> strogo = new ArrayList<Boolean>();
					File myObj = new File("src/pokrivenost.txt");
					FileWriter pokrivenost;
					if (myObj.createNewFile()) {
						System.out.println("File created: " + myObj.getName());
						pokrivenost = new FileWriter("src/pokrivenost.txt");

						pokrivenostMethod(slabo, strogo, pokrivenost);
						// pokrivenost.close();

					} else {
						System.out.println("File already exists.");
						pokrivenost = new FileWriter("src/pokrivenost.txt");

						pokrivenostMethod(slabo, strogo, pokrivenost);
						// pokrivenost.close();

//						pokrivenost.write("originalni rezultat: " + listaRezultata.get(0) + "\n");
//						if (!listaRezultata.get(0).contains("exception")
//								&& !listaRezultata.get(0).contains("Opperation")) {
//							for (int i = 1; i < listaRezultata.size(); i++) {
//								if (listaRezultata.get(0).equals(listaRezultata.get(i))) {
//									pokrivenost.write("test slabo pokriva mutanta " + "M" + i);
//									pokrivenost.write("sa rezultatom: " + listaRezultata.get(i) + "\n");
//									slabo = true;
//								} else {
//									pokrivenost.write("test strogo pokriva mutanta " + "M" + i);
//									pokrivenost.write("sa rezultatom: " + listaRezultata.get(i) + "\n");
//									strogo = true;
//								}
//								pokrivenost.flush();
//							}
//							pokrivenost.close();
//						} else {
//							pokrivenost.write("original code has an exception or didn't execute well");
//							pokrivenost.flush();
//							pokrivenost.close();
//						}
					}
					
					boolean slaboTrue = true;
					boolean strogoTrue = true;
					
					for (int i = 0; i < slabo.size(); i++) {
						if(slabo.get(i)==false) {
							slaboTrue = false;
						}
					}
					
					for (int i = 0; i < strogo.size(); i++) {
						if(strogo.get(i)==false) {
							strogoTrue = false;
						}
					}
					
//					if (slaboTrue) {
//						pokrivenost.write("skup testova slabo pokriva skup mutanata\n");
//					}
					if (strogoTrue) {
						pokrivenost.write("skup testova strogo pokriva skup mutanata\n");
					}
					if(!strogoTrue) {
						pokrivenost.write("skup testova ne pokriva strogo skup mutanata\n");
					}
					pokrivenost.flush();
					pokrivenost.close();
				} catch (IOException e) {
					System.out.println("An error occurred.");
					e.printStackTrace();
				}
			} catch (Exception e) {
				e.printStackTrace();
			}
		} catch (

		Exception e) {
			e.printStackTrace();
		}
	}

	private static void pokrivenostMethod(List<Boolean> slabo, List<Boolean> strogo, FileWriter pokrivenost)
			throws IOException {
		for (int i1 = 0; i1 < vrednostiPromenljivih.entrySet().size(); i1++) {
			//int brojMutanata = sviMutanti.size();//vrednostiPromenljivih.get("test" + (i1 + 1)).size();

			try{
				pokrivenost.write("test" + (i1 + 1) +" originalni rezultat: " + listaRezultata.get((i1 + 1)).get(0) + "\n");
			
			
			if (!listaRezultata.get((i1 + 1)).get(0).contains("exception")
					&& !listaRezultata.get((i1 + 1)).get(0).contains("Opperation")) {
				for (int i = 1; i < listaRezultata.get((i1 + 1)).size(); i++) {
					if (listaRezultata.get((i1 + 1)).get(0)
							.equals(listaRezultata.get((i1 + 1)).get(i))) {
						pokrivenost.write("test" + (i1 + 1) + " ne pokriva strogo mutanta " + "M" + i);
						pokrivenost.write(" sa rezultatom: " + listaRezultata.get((i1 + 1)).get(i) + "\n");
						if(slabo.size()>i-1) {
							slabo.set(i-1, true);
						} else {
							slabo.add(true);
							strogo.add(false);
						}
					} else {
						pokrivenost.write("test" + (i1 + 1) + " strogo pokriva mutanta " + "M" + i);
						pokrivenost.write(" sa rezultatom: " + listaRezultata.get((i1 + 1)).get(i) + "\n");
						if(strogo.size()>i-1) {
							strogo.set(i-1, true);
						} else {
							strogo.add(true);
							slabo.add(false);
						}
					}
					pokrivenost.flush();
				}
				pokrivenost.write("");
				pokrivenost.flush();
			} else {
				pokrivenost.write("original code has an exception or didn't execute well try modifying your input files");
				pokrivenost.write("");
				pokrivenost.flush();
			}
			
			} catch(Exception e) {
				System.out.println("your input files are probably bad");
				pokrivenost.write("your input files are probably bad");
				pokrivenost.write("");
				pokrivenost.flush();
			}
		}
	}

	private static void writeToFileAndExecute(File myObj, List<String> mutantiVarList, String methodName,
			TreeMap<String, ArrayList<String>> vrednostiPromenljivih) {

		for (int i = 0; i < mutantiVarList.size(); i++) {

			try {
				FileWriter myWriter = new FileWriter("src/TestMutant.java");
				myWriter.write("public class TestMutant {\n");
				myWriter.write(mutantiVarList.get(i));
				myWriter.write("public static void main(String[] args){\n");
				myWriter.write("\ntry{" + "\n\t\tSystem.out.println(" + "TestMutant." + methodName + "("
						+ poredjajPromenljive(vrednostiPromenljivih.get("test1")) + "));" + "\n" + "} catch(Exception e"
						+ i + "){\n\t\tSystem.out.println(\"There was an exception \" + e" + i + ".getMessage());\n}");

				myWriter.write("\n}\n}");
				myWriter.close();
				try {
					int exitCode = runProcess("javac src/TestMutant.java");
					if (exitCode != 0) {
						mutantiVarList.remove(i);
						i--;
					}
				} catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}

			} catch (IOException e) {
				System.out.println("An error occurred.");
				e.printStackTrace();
			}

		}
	}

	private static void writeToFileAndExecuteJavaVM(File myObj, List<String> sviMutanti, String methodName,
			ArrayList<String> vrednostiPromenljivihInner) {
		for (int i = 0; i < sviMutanti.size(); i++) {
			try {
				FileWriter myWriter = new FileWriter("src/Mutant.java");
				myWriter.write("public class Mutant {\n");
				myWriter.write(sviMutanti.get(i));
				myWriter.write("public static void main(String[] args){\n");
				myWriter.write("\ntry{" + "\n\t\tSystem.out.println(" + "Mutant." + methodName + "("
						+ poredjajPromenljive(vrednostiPromenljivihInner) + "));" + "\n" + "} catch(Exception e" + i
						+ "){\n\t\tSystem.out.println(\"There was an exception \" + e" + i + ".getMessage());\n}");

				myWriter.write("\n}\n}");
				myWriter.flush();
				myWriter.close();
				try {
					runProcess("java src/Mutant.java");
				} catch (Exception e) {
					// TODO Auto-generated catch block
					System.out.println("Exception: " + e.getMessage());

				}

			} catch (IOException e) {
				System.out.println("An error occurred.");
				e.printStackTrace();
			}

		}
	}

	private static void writeToFileAndExecuteJavaVMorg(File myObj, List<String> original, String methodName,
			ArrayList<String> vrednostiPromenljivihInner) {

		for (int i = 0; i < original.size(); i++) {
			try {
				FileWriter myWriter = new FileWriter("src/Mutant.java");
				myWriter.write("public class Mutant {\n");
				myWriter.write(original.get(i));
				myWriter.write("public static void main(String[] args){\n");
				myWriter.write("\ntry{" + "\n\t\tSystem.out.println(" + "Mutant." + methodName + "("
						+ poredjajPromenljive(vrednostiPromenljivihInner) + "));" + "\n" + "} catch(Exception e" + i
						+ "){\n\t\tSystem.out.println(\"There was an exception \" + e" + i + ".getMessage());\n}");

				myWriter.write("\n}\n}");
				myWriter.close();
				try {
					runProcess("java src/Mutant.java");
				} catch (Exception e) {
					// TODO Auto-generated catch block
					System.out.println("Exception: " + e.getMessage());

				}

			} catch (IOException e) {
				System.out.println("An error occurred.");
				e.printStackTrace();
			}
		}
	}

	private static void printLines(String cmd, InputStream ins) throws Exception {
		String line = null;
		BufferedReader in = new BufferedReader(new InputStreamReader(ins));
		while ((line = in.readLine()) != null) {
			System.out.println(cmd + " " + line);
			if (cmd.substring(cmd.length() - 4).equals("out:")) {
				if (listaRezultata.containsKey(test)) {
					listaRezultata.get(test).add(line);
				} else {
					listaRezultata.put(test, new ArrayList<String>());
					listaRezultata.get(test).add(line);
				}
			}
		}
	}

	private static void printLines(String cmd, String ins) throws Exception {
		System.out.println(cmd + " " + ins);
		if (listaRezultata.containsKey(test)) {
			listaRezultata.get(test).add(ins);
		} else {
			listaRezultata.put(test, new ArrayList<String>());
			listaRezultata.get(test).add(ins);
		}
	}

	private static int runProcess(String command) throws Exception {
		Process pro = Runtime.getRuntime().exec(command);
		// printLines(command + " stdout:", pro.getInputStream());
		// printLines(command + " stderr:", pro.getErrorStream());
		// printLines(pro.getInputStream());
		// printLines(pro.getErrorStream());
		if (!pro.waitFor(3, TimeUnit.SECONDS)) {
			printLines(command + " stdout:", "Opperation stopped after 3 seconds");
			pro.destroy();
			//printLines(command + " stderr:", pro.getErrorStream());
		} else {
			printLines(command + " stdout:", pro.getInputStream());
			printLines(command + " stderr:", pro.getErrorStream());
		}
		System.out.println(command + " exitValue() " + pro.exitValue());
		return pro.exitValue();
	}

	private static void findValues(TreeMap<String, ArrayList<String>> vrednostiPromenljivih, Scanner primerReader) {
		// TODO Auto-generated method stub
		int counter = 0;
		while (primerReader.hasNextLine()) {
			String data = primerReader.nextLine();
			if (data.contains("test")) {
				counter++;
				continue;
			}
			String vrednost = data.trim().split("=")[1].trim();
			vrednost = vrednost.substring(0, vrednost.length() - 1);
			if (!vrednostiPromenljivih.containsKey("test" + counter)) {
				vrednostiPromenljivih.put("test" + counter, new ArrayList<String>());
				vrednostiPromenljivih.get("test" + counter).add(vrednost);
			} else {
				vrednostiPromenljivih.get("test" + counter).add(vrednost);
			}
		}
	}

	// List<String> vrednostiPromenljivih,
	// vrednostiPromenljivih.add(vrednost.substring(0, vrednost.length() - 1));
	private static void findVariables(TreeMap<String, ArrayList<String>> naziviPromenljivihIstogTipa,
			Scanner myReader) {
		String data;
		while (myReader.hasNextLine()) {
			data = myReader.nextLine();
			// data.trim().substring(data.trim().length()-1,data.trim().length()).toString().equals(";")
			// &&
			if (data.trim().length() > 1 && ((data.trim().length() > 4 && data.trim().substring(0, 4).equals("int "))
					|| (data.trim().length() > 6 && data.trim().substring(0, 6).equals("float "))
					|| (data.trim().length() > 7 && data.trim().substring(0, 7).equals("double "))
					|| (data.trim().length() > 5 && data.trim().substring(0, 5).equals("long "))
					|| (data.trim().length() > 8 && data.trim().substring(0, 8).equals("boolean "))
					|| (data.trim().length() > 4 && data.trim().substring(0, 4).equals("int["))
					|| (data.trim().length() > 6 && data.trim().substring(0, 6).equals("float["))
					|| (data.trim().length() > 7 && data.trim().substring(0, 7).equals("double["))
					|| (data.trim().length() > 5 && data.trim().substring(0, 5).equals("long["))
					|| (data.trim().length() > 8 && data.trim().substring(0, 8).equals("boolean["))))

			{

				// if(vrednost.contains("[")) {
				// int indexVred = vrednost.indexOf("[");
				// vrednost = vrednost.substring(0,indexVred+1) +
				// vrednost.substring(indexVred+2,vrednost.length());
				// }
				if (data.trim().substring(0, 4).equals("int ")) {
					if (!naziviPromenljivihIstogTipa.containsKey("int")) {
						naziviPromenljivihIstogTipa.put("int", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("int").add(data.trim().split(" ")[1]);
					} else {
						naziviPromenljivihIstogTipa.get("int").add(data.trim().split(" ")[1]);
					}
				}
				if (data.trim().substring(0, 6).equals("float ")) {
					if (!naziviPromenljivihIstogTipa.containsKey("float")) {
						naziviPromenljivihIstogTipa.put("float", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("float").add(data.trim().split(" ")[1]);
					} else {
						naziviPromenljivihIstogTipa.get("float").add(data.trim().split(" ")[1]);
					}
				}
				if (data.trim().substring(0, 7).equals("double ")) {
					if (!naziviPromenljivihIstogTipa.containsKey("double")) {
						naziviPromenljivihIstogTipa.put("double", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("double").add(data.trim().split(" ")[1]);
					} else {
						naziviPromenljivihIstogTipa.get("double").add(data.trim().split(" ")[1]);
					}
				}
				if (data.trim().substring(0, 5).equals("long ")) {
					if (!naziviPromenljivihIstogTipa.containsKey("long")) {
						naziviPromenljivihIstogTipa.put("long", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("long").add(data.trim().split(" ")[1]);
					} else {
						naziviPromenljivihIstogTipa.get("long").add(data.trim().split(" ")[1]);
					}
				}
				if (data.trim().substring(0, 8).equals("boolean ")) {
					if (!naziviPromenljivihIstogTipa.containsKey("boolean")) {
						naziviPromenljivihIstogTipa.put("boolean", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("boolean").add(data.trim().split(" ")[1]);
					} else {
						naziviPromenljivihIstogTipa.get("boolean").add(data.trim().split(" ")[1]);
					}
				}
// $$$$$$$$$################################33
				if (data.trim().substring(0, 4).equals("int[")) {
					if (!naziviPromenljivihIstogTipa.containsKey("int[]")) {
						naziviPromenljivihIstogTipa.put("int[]", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("int[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(6, data.length() - 1).trim());
					} else {
						naziviPromenljivihIstogTipa.get("int[]").add(data.trim().split(" ")[1] + "[");
						// add(data.substring(6, data.length() - 1).trim());
					}
				}
				if (data.trim().substring(0, 6).equals("float[")) {
					if (!naziviPromenljivihIstogTipa.containsKey("float[]")) {
						naziviPromenljivihIstogTipa.put("float[]", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("float[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(8, data.length() - 1).trim());
					} else {
						naziviPromenljivihIstogTipa.get("float[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(8, data.length() - 1).trim());
					}
				}
				if (data.trim().substring(0, 7).equals("double[")) {
					if (!naziviPromenljivihIstogTipa.containsKey("double[]")) {
						naziviPromenljivihIstogTipa.put("double[]", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("double[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(9, data.length() - 1).trim());
					} else {
						naziviPromenljivihIstogTipa.get("double[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(9, data.length() - 1).trim());
					}
				}
				if (data.trim().substring(0, 5).equals("long[")) {
					if (!naziviPromenljivihIstogTipa.containsKey("long[]")) {
						naziviPromenljivihIstogTipa.put("long[]", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("long[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(7, data.length() - 1).trim());
					} else {
						naziviPromenljivihIstogTipa.get("long[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(7, data.length() - 1).trim());
					}
				}
				if (data.trim().substring(0, 8).equals("boolean[")) {
					if (!naziviPromenljivihIstogTipa.containsKey("boolean[]")) {
						naziviPromenljivihIstogTipa.put("boolean[]", new ArrayList<String>());
						naziviPromenljivihIstogTipa.get("boolean[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(10, data.length() - 1).trim());
					} else {
						naziviPromenljivihIstogTipa.get("boolean[]").add(data.trim().split(" ")[1] + "[");
						// .add(data.substring(10, data.length() - 1).trim());
					}
				}

			}

			// #####################################################33
			if (data.contains("(")) {
				int indBrack = data.indexOf("(");
				data = data.substring(indBrack);
				String dataPom = "";
				while (data.contains("int ")) {
					int index = data.indexOf("int ");
					if (!naziviPromenljivihIstogTipa.containsKey("int")) {
						naziviPromenljivihIstogTipa.put("int", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("int")[1].trim().split(" ")[0];
					if (dataPom.substring(dataPom.length() - 1).equals(",")
							|| dataPom.substring(dataPom.length() - 1).equals(")")) {
						dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					}
					if (dataPom.contains(" ")) {
						dataPom = dataPom.split(" ")[0].trim();
					}
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0].trim();
					}
					if (!naziviPromenljivihIstogTipa.get("int").contains(dataPom))
						naziviPromenljivihIstogTipa.get("int").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 3, data.length());
				}

				while (data.contains("float ")) {
					int index = data.indexOf("float ");
					if (!naziviPromenljivihIstogTipa.containsKey("float")) {
						naziviPromenljivihIstogTipa.put("float", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("float")[1].trim().split(" ")[0];
					if (dataPom.substring(dataPom.length() - 1).equals(",")
							|| dataPom.substring(dataPom.length() - 1).equals(")")) {
						dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					}
					if (dataPom.contains(" ")) {
						dataPom = dataPom.split(" ")[0].trim();
					}
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0].trim();
					}
					if (!naziviPromenljivihIstogTipa.get("float").contains(dataPom))
						naziviPromenljivihIstogTipa.get("float").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 5, data.length());
				}

				while (data.contains("double ")) {
					int index = data.indexOf("double ");
					if (!naziviPromenljivihIstogTipa.containsKey("double")) {
						naziviPromenljivihIstogTipa.put("double", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("double")[1].trim().split(" ")[0];
					if (dataPom.substring(dataPom.length() - 1).equals(",")
							|| dataPom.substring(dataPom.length() - 1).equals(")")) {
						dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					}
					if (dataPom.contains(" ")) {
						dataPom = dataPom.split(" ")[0].trim();
					}
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0].trim();
					}
					if (!naziviPromenljivihIstogTipa.get("double").contains(dataPom))
						naziviPromenljivihIstogTipa.get("double").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 6, data.length());
				}
				while (data.contains("long ")) {
					int index = data.indexOf("long ");
					if (!naziviPromenljivihIstogTipa.containsKey("long")) {
						naziviPromenljivihIstogTipa.put("long", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("long")[1].trim().split(" ")[0];
					if (dataPom.substring(dataPom.length() - 1).equals(",")
							|| dataPom.substring(dataPom.length() - 1).equals(")")) {
						dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					}
					if (dataPom.contains(" ")) {
						dataPom = dataPom.split(" ")[0].trim();
					}
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0].trim();
					}
					if (!naziviPromenljivihIstogTipa.get("long").contains(dataPom))
						naziviPromenljivihIstogTipa.get("long").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 4, data.length());
				}

				while (data.contains("boolean ")) {
					int index = data.indexOf("boolean ");
					if (!naziviPromenljivihIstogTipa.containsKey("boolean")) {
						naziviPromenljivihIstogTipa.put("boolean", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("boolean")[1].trim().split(" ")[0];
					if (dataPom.substring(dataPom.length() - 1).equals(",")
							|| dataPom.substring(dataPom.length() - 1).equals(")")) {
						dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					}
					if (dataPom.contains(" ")) {
						dataPom = dataPom.split(" ")[0].trim();
					}
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0].trim();
					}
					if (!naziviPromenljivihIstogTipa.get("boolean").contains(dataPom))
						naziviPromenljivihIstogTipa.get("boolean").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 7, data.length());
				}

				// ##################################################################

				while (data.contains("int[")) {
					int index = data.indexOf("int[");
					if (!naziviPromenljivihIstogTipa.containsKey("int[]")) {
						naziviPromenljivihIstogTipa.put("int[]", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("int\\[\\]")[1].split(" ")[1];
					dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0];
					}
					dataPom += "[";
					if (!naziviPromenljivihIstogTipa.get("int[]").contains(dataPom))
						naziviPromenljivihIstogTipa.get("int[]").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 4, data.length());
				}
				while (data.contains("float[")) {
					int index = data.indexOf("float[");
					if (!naziviPromenljivihIstogTipa.containsKey("float[]")) {
						naziviPromenljivihIstogTipa.put("float[]", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("float\\[\\]")[1].split(" ")[1];
					dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0];
					}
					dataPom += "[";
					if (!naziviPromenljivihIstogTipa.get("float[]").contains(dataPom))
						naziviPromenljivihIstogTipa.get("float[]").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 6, data.length());
				}
				while (data.contains("double[")) {
					int index = data.indexOf("double[");
					if (!naziviPromenljivihIstogTipa.containsKey("double[]")) {
						naziviPromenljivihIstogTipa.put("double[]", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("double\\[\\]")[1].split(" ")[1];
					dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0];
					}
					dataPom += "[";
					if (!naziviPromenljivihIstogTipa.get("double[]").contains(dataPom))
						naziviPromenljivihIstogTipa.get("double[]").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 7, data.length());
				}
				while (data.contains("long[")) {
					int index = data.indexOf("long[");
					if (!naziviPromenljivihIstogTipa.containsKey("long[]")) {
						naziviPromenljivihIstogTipa.put("long[]", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("long\\[\\]")[1].split(" ")[1];
					dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0];
					}
					dataPom += "[";
					if (!naziviPromenljivihIstogTipa.get("long[]").contains(dataPom))
						naziviPromenljivihIstogTipa.get("long[]").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 5, data.length());
				}
				while (data.contains("boolean[")) {
					int index = data.indexOf("boolean[");
					if (!naziviPromenljivihIstogTipa.containsKey("boolean[]")) {
						naziviPromenljivihIstogTipa.put("boolean[]", new ArrayList<String>());
					}
					dataPom = data.substring(index).split("boolean\\[\\]")[1].split(" ")[1];
					dataPom = dataPom.trim().substring(0, dataPom.trim().length() - 1);
					if (dataPom.contains("=")) {
						dataPom = dataPom.split("=")[0];
					}
					dataPom += "[";
					if (!naziviPromenljivihIstogTipa.get("boolean[]").contains(dataPom))
						naziviPromenljivihIstogTipa.get("boolean[]").add(dataPom);
					data = data.substring(0, index) + " " + data.substring(index + 8, data.length());
				}
				// command = "java src/Test.java " + data;
				// runProcess(command);
			}
		}
		myReader.close();
	}

	private static String poredjajPromenljive(List<String> vrednostiPromenljivih) {
		String rez = "";
		for (int i = 0; i < vrednostiPromenljivih.size() - 1; i++) {
			rez += vrednostiPromenljivih.get(i) + ", ";
		}
		rez += vrednostiPromenljivih.get(vrednostiPromenljivih.size() - 1);
		return rez;
	}

}
