// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function DeleteExam(examid) {
    $.post("/Panel/DeleteExam", { examid: examid }, function (data) {
        if (data != "OK") {
            alert(data);
            return false;
        }
        $('#examid-' + examid).remove();
        alert("Exam was deleted")

    });
}

$(document).ready(function () {

    $('#ContentId').on('change', function () {
        var contentid = $(this).val();
        $.post("/Panel/GetContentDetail", { contentid: contentid }, function (data) {
            $('#content-text').html(data.contentText);
        });
    });
    $('#btnCreate').click(function () {
        var contentid = $('#ContentId').val();
        if (contentid == "0") {
            alert("Please Select Content!");
            return false;
        }
        const questions = [];
        // "save it to a variable"
       

        for (var i = 1; i < 5; i++) {
            var text = $('#Question-' + i).val();
            var A = $('#txtQ-' + i + '-A').val();
            var B = $('#txtQ-' + i + '-B').val();
            var C = $('#txtQ-' + i + '-C').val();
            var D = $('#txtQ-' + i + '-D').val();
            var answer = $('#answer-' + i).val();
            if (A == "" || B == "" || C == "" || D == "") {
                alert("Please Fill All Options");
                return false;
            }
            if (answer == "") {
                alert("Please Select Answer");
                return false;
            }
            questions.push({
                ExamId : contentid,
                Text :text,
                A : A,
                B : B,
                C : C,
                D : D,
                Answer: answer
            });
        }

        $.post("/Panel/CreateExam", { questions: questions }, function (data) {
            if (data!=true) {
                alert(data);
                return false;
            }
            window.location.href = "/Panel";

        });
    });
    $('#btnFinish').click(function () {
        $('#btnFinish').prop('disabled', 'disabled');
        var examid = $('#hfExamId').val();
       
        const answers = [];
        for (var i = 1; i < 5; i++) {
           
            var QuestionId = $('#hfQuestion-'+i).val();
            var answer = $('input[name="Question-'+i+'"]:checked').val();
                answers.push({
                ExamId: examid,
                id: QuestionId,
                Answer: answer
            });
        }
        var model = {
            ExamId: examid,
            Answers:answers 
        }
        $.post("/User/FinishExam", { answers: model }, function (data) {
            var ans = data.answers;
            $.each(ans, function (index, value) {
                if (value.true)
                    $('#txtQ-' + value.trueAnswer + '-' + value.id).parent().addClass('trueanswer');
                else {
                    $('#txtQ-' + value.answer + '-' + value.id).parent().addClass('wronganswer');
                    $('#txtQ-' + value.trueAnswer + '-' + value.id).parent().addClass('trueanswer');
                }
            });
          
        });
    });
    
});