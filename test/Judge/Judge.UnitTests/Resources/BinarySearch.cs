public class Solution
{
    public int BinarySearch(int[] arr, int n)
    {
        if (arr == null || arr.length == 0) return -1;

        int lo = 0;
        int hi = arr.length - 1;

        while (lo <= hi)
        {
            int mid = lo + (hi - lo) / 2;
            if (n < arr[mid])
            {
                hi = mid - 1;
            }
            else if (n > arr[mid])
            {
                lo = mid + 1;
            }
            else
            {
                return mid;
            }
        }

        return -1;
    }
}
