namespace Sintactico {
    /// Tipos de token habiles
    public enum tk_t {
        P_IF, P_THEN, P_ELSE, P_REPEAT, P_UNTIL, P_WHILE, P_END, P_MAIN, P_FLOAT, P_INT, P_BOOLEAN, P_TRUE, P_FALSE,
        IDE, N_INT, N_FLOAT, O_ASIGNACION, O_IGUALDAD, O_MAYOR, O_MENOR, O_MAY_I, O_MEN_I, O_DIFERENTE,
        O_MAS, O_MENOS, O_MUL, O_MODULO, O_INC, O_DEC, O_DIV, B_PA, B_PC, B_CA, B_CC, B_COMA, B_PYC,
        O_CIN, O_COUT, CMT, CMTS, NULL, ERROR, PROGRAMA
    }
    /// Tipos de nodos
    public enum nd_t {
        SENT, DECL, ASIGN, TYPE, ID, ERROR_SEM, FLOAT, INT, BOOLEAN
    }
    /// P code papu
    public enum opCodeTab {
        HALT, IN, OUT, ADD, SUB, MUL, DIV,// RR CODES
        LD, STO, // RM CODES
        LDA, LDC, JLT, JLE, JGT, JGE, JEQ, JNE,// RA CODES
        LABEL, JUMP, F_JUMP, LDB, LDF, LDI//extras (nuevos)
    };
    public enum stepResultTab { OK, Halted, Instruction_Memory_Fault, Data_Memory_Fault, Division_by_0 };

    /*
     HALT, IN, OUT, ADD, SUB, MUL, DIV,// RR CODES
        LD, STO, // RM CODES
        LDA, LDC, JLT, JLE, JGT, JGE, JEQ, JNE,// RA CODES
        LABEL, JUMP, F_JUMP, LDB, LDF, LDI//extras
     */
}
