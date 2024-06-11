namespace MyStorageApplication.StorageManager.Domain.Helpers
{
    public static class MessagesHelper
    {
        public static readonly string MESSAGE_NOT_FOUND = "{0} não encontrado";
        public static readonly string MESSAGE_FIELD_REQUERID = "O campo {0} é obrigatório";
        public static readonly string MESSAGE_NUMBER_VALUE_REQUERID = "O campo {0} deve possuir um valor maior que 0";
        public static readonly string MESSAGE_INVALID_TYPE_OF_MOVEMENTATION = "Tipo de movimentação invalido, selecione E (Entrada) ou S (Saída)";
        public static readonly string MESSAGE_INSUFFICIENT_STOCK_BALANCE = "Saldo em estoque insuficiente";
        public static readonly string MESSAGE_INSUFFICIENT_STOCK_BALANCE_SELECTED = "Não há saldo para o estoque seleciondo para este produto";
    }
}
