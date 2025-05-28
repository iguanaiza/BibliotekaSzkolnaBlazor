/*window.initAddBookValidation = () => {
    $(document).ready(function () {
        // ISBN z pauzami
        $('#isbn-add').on('input', function () {
            let raw = $(this).val().replace(/\D/g, '').substring(0, 13);
            let formatted = raw;
            if (raw.length >= 3) formatted = raw.slice(0, 3);
            if (raw.length >= 5) formatted += '-' + raw.slice(3, 5);
            if (raw.length >= 9) formatted += '-' + raw.slice(5, 9);
            if (raw.length >= 12) formatted += '-' + raw.slice(9, 12);
            if (raw.length === 13) formatted += '-' + raw.slice(12);
            $(this).val(formatted);
        });

        // Rok: tylko cyfry, max 4
        $('#year-add').on('input', function () {
            this.value = this.value.replace(/\D/g, '').substring(0, 4);
        });

        // Strony: tylko cyfry, max 4
        $('#pages-add').on('input', function () {
            this.value = this.value.replace(/\D/g, '').substring(0, 4);
        });
    });
};
*/