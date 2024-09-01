using System;

class Program
{
    static void Main()
    {
        // Step 1: Define Criteria
        string[] criteria = { "ROI", "Strategic Alignment", "Risk", "Time to Market" };

        // Step 2: Pairwise Comparison Matrix (Saaty Scale)
        double[,] pairwiseMatrix = {
            { 1, 3, 5, 7 }, // ROI
            { 1.0/3.0, 1, 3, 5 }, // Strategic Alignment
            { 1.0/5.0, 1.0/3.0, 1, 3 }, // Risk
            { 1.0/7.0, 1.0/5.0, 1.0/3.0, 1 } // Time to Market
        };

        // Step 3: Calculate Priority Vector (Normalized Eigenvector)
        double[] priorityVector = CalculatePriorityVector(pairwiseMatrix, criteria.Length);

        // Step 4: Display the priority vector
        Console.WriteLine("Priority Vector:");
        for (int i = 0; i < criteria.Length; i++)
        {
            Console.WriteLine($"{criteria[i]}: {priorityVector[i]:F4}");
        }

        // Step 5: Perform Project Prioritization based on weighted criteria
        // Example scores for three projects on the criteria
        double[] project1Scores = { 0.9, 0.7, 0.8, 0.6 };
        double[] project2Scores = { 0.8, 0.8, 0.6, 0.7 };
        double[] project3Scores = { 0.7, 0.9, 0.7, 0.8 };

        double project1FinalScore = CalculateFinalScore(project1Scores, priorityVector);
        double project2FinalScore = CalculateFinalScore(project2Scores, priorityVector);
        double project3FinalScore = CalculateFinalScore(project3Scores, priorityVector);

        Console.WriteLine($"\nProject 1 Final Score: {project1FinalScore:F4}");
        Console.WriteLine($"Project 2 Final Score: {project2FinalScore:F4}");
        Console.WriteLine($"Project 3 Final Score: {project3FinalScore:F4}");

        // Step 6: Determine the best project
        string bestProject = DetermineBestProject(project1FinalScore, project2FinalScore, project3FinalScore);
        Console.WriteLine($"\nThe best project to prioritize is: {bestProject}");
    }

    static double[] CalculatePriorityVector(double[,] matrix, int size)
    {
        double[] sumOfColumns = new double[size];
        double[] normalizedMatrixSum = new double[size];
        double[] priorityVector = new double[size];

        // Calculate the sum of each column
        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < size; i++)
            {
                sumOfColumns[j] += matrix[i, j];
            }
        }

        // Normalize the matrix
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                normalizedMatrixSum[i] += (matrix[i, j] / sumOfColumns[j]);
            }
            // Calculate the priority vector by averaging the rows of the normalized matrix
            priorityVector[i] = normalizedMatrixSum[i] / size;
        }

        return priorityVector;
    }

    static double CalculateFinalScore(double[] scores, double[] priorityVector)
    {
        double finalScore = 0.0;
        for (int i = 0; i < scores.Length; i++)
        {
            finalScore += scores[i] * priorityVector[i];
        }
        return finalScore;
    }

    static string DetermineBestProject(double project1Score, double project2Score, double project3Score)
    {
        if (project1Score > project2Score && project1Score > project3Score)
        {
            return "Project 1";
        }
        else if (project2Score > project1Score && project2Score > project3Score)
        {
            return "Project 2";
        }
        else
        {
            return "Project 3";
        }
    }
}