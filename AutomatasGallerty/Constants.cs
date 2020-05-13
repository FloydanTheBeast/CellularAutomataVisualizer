namespace AutomatasGallery
{
    public static class Constants
    {
        public static int CellSize;

        public static int[][] MooreNeighborhood;

        public static int[][] NeumannNeigborhood;

        public static int[][] ThreeBitsNeigborhood;

        static Constants()
        {
            CellSize = 8;

            MooreNeighborhood = new[]
            {
                new [] {-1, -1},
                new [] {-1, 0},
                new [] {-1, 1},
                new [] {0, -1},
                new [] {0, 0},
                new [] {0, 1},
                new [] {1, -1},
                new [] {1, 0},
                new [] {1, 1},
            };

            NeumannNeigborhood = new[]
            {
                new [] {0, -1},
                new [] {-1, 0},
                new [] {0, 0},
                new [] {0, 1},
                new [] {1, 0},
            };

            ThreeBitsNeigborhood = new[]
            {
                new [] {-1},
                new [] {0},
                new [] {1},
            };
        }
    }
}
