package ad;

public class Vector {
	
	public static void main(String[] args){
		int[] v ={2,3,5,23,6,0,21,44,9,20};
		min(v);
		
	}
	public static int min(int [] v){
		int minValue = v[0];
		for( int i =0;i<v.length;i++){
			if(v[i]<minValue){
				minValue=v[i];
			}
		}
		
		System.out.println(minValue);
		return minValue;
	}


}
