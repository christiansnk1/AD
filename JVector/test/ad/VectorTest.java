package ad;

import static org.junit.Assert.*;

import org.junit.Test;

import ad.Vector;

public class VectorTest {

	@Test
	public void test() {
		int [] vector = {33,16,12,15,19,24,9,88,2,10};
		int minValue = Vector.min(vector);
		assertEquals(2,minValue);
	}

}
